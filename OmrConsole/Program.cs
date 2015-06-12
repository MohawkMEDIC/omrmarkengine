/* 
 * Optical Mark Recognition 
 * Copyright 2015, Justin Fyfe
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * Author: Justin
 * Date: 6-12-2015
 */

using MohawkCollege.Util.Console.Parameters;
using OmrMarkEngine.Core;
using OmrMarkEngine.Core.Processor;
using OmrMarkEngine.Output;
using OmrMarkEngine.Output.Transforms;
using OmrMarkEngine.Template;
using OmrMarkEngine.Template.Scripting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OmrConsole
{
    public class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Optical Mark Engine Console Utility");
                Console.WriteLine("Version {0}, {1}", Assembly.GetEntryAssembly().GetName().Version, Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute)).Select(o=>o as AssemblyCopyrightAttribute).First().Copyright);

                // Parse parameters
                var parser = new ParameterParser<ConsoleParameters>();
                var parameters = parser.Parse(args);

                // Show help?
                bool showHelp = String.IsNullOrEmpty(parameters.OutputFile) || String.IsNullOrEmpty(parameters.Template) || parameters.SourceFiles == null ||
                    parameters.Help;
                if (showHelp)
                    parser.WriteHelp(Console.Out);
                else if (!File.Exists(parameters.Template))
                    throw new ArgumentException(String.Format("{0} not found", parameters.Template));
                else if (parameters.OutputFormat.ToLower() != "csv" && parameters.OutputFormat.ToLower() != "xml")
                    throw new ArgumentException("Output format must be XML or CSV!");

                // Load the files
                foreach (var ptrn in parameters.SourceFiles)
                {
                    String directory = Path.GetDirectoryName(ptrn) ?? ".",
                        pattern = Path.GetFileName(ptrn);

                    // Get files
                    OmrPageOutputCollection outputCollection = new OmrPageOutputCollection();
                    Engine engine = new Engine();
                    TemplateScriptUtil tsu = new TemplateScriptUtil();
                    var template = OmrTemplate.Load(parameters.Template);


                    // Process files
                    foreach (var fileName in Directory.GetFiles(directory, pattern))
                    {
                        Console.Write("Processing {0}...", Path.GetFileName(fileName));
                        using (ScannedImage sci = new ScannedImage(fileName))
                        {
                            sci.Analyze();
                            if(sci.TemplateName != template.Id)
                            {
                                Console.WriteLine("Wrong template!");
                                continue;
                            }

                            var output = engine.ApplyTemplate(template, sci);
                            var validate = output.Validate(template);
                            if (parameters.Validate && !validate.IsValid)
                            {
                                Console.WriteLine("Invalid");
                                foreach (var iss in validate.Issues)
                                    Console.WriteLine("\t{0}", iss);
                            }
                            else
                            {
                                try
                                {
                                    if (parameters.Run)
                                        tsu.Run(template, output);
                                    outputCollection.Pages.Add(output);
                                    Console.WriteLine("Success");
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine("Error\r\n\t{0}", e.Message);
                                }
                            }
                        }
                    }

                    // Save the output?
                    switch(parameters.OutputFormat)
                    {
                        case "csv":
                            {
                                var outputBytes = new CsvOutputTransform().Transform(template, outputCollection);
                                using (FileStream fs = File.Create(parameters.OutputFile))
                                {
                                    fs.Write(outputBytes, 0, outputBytes.Length);
                                }
                                break;
                            }
                        default:
                            {
                                var outputBytes = new RawOutputTransform().Transform(template, outputCollection);
                                using (FileStream fs = File.Create(parameters.OutputFile))
                                {
                                    fs.Write(outputBytes, 0, outputBytes.Length);
                                }
                            }
                            break;

                    }
                }
#if DEBUG
                Console.ReadKey();
#endif
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Error: {0}", e.Message);
                Environment.Exit(-1);
            }
        }
    }
}
