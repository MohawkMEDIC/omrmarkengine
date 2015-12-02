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
 * Date: 4-17-2015
 */
using OmrMarkEngine.Core.Template.Scripting.Forms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmrMarkEngine.Template.Scripting.Util
{
    /// <summary>
    /// Represents a utility for calling rest functions and interpreting the results
    /// </summary>
    public class RestUtil
    {

        /// <summary>
        /// The base uri
        /// </summary>
        private Uri m_baseUri;

        // Trusted certs
        private static List<String> s_trustedCerts = new List<String>();

        private static String s_username = null;
        private static String s_password = null;

        public String GetCurrentUserName { get { return s_username; } }

        /// <summary>
        /// Creates a new instance of the rest utility
        /// </summary>
        public RestUtil(Uri baseUri)
        {
            ServicePointManager.ServerCertificateValidationCallback = RestCertificateValidation;
            this.m_baseUri = baseUri;
        }

        /// <summary>
        /// Validate the REST certificate
        /// </summary>
        static bool RestCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors error)
        {
            if (certificate == null || chain == null)
                return false;
            else
            {
                var valid = s_trustedCerts.Contains(certificate.Subject);
                if(!valid && (chain.ChainStatus.Length > 0 || error != SslPolicyErrors.None))
                    if(MessageBox.Show(String.Format("The remote certificate is not trusted. The error was {0}. The certificate is: \r\n{1}\r\nWould you like to temporarily trust this certificate?", error, certificate.Subject), "Certificate Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        return false;
                    else
                        s_trustedCerts.Add(certificate.Subject);

                return true;
                //isValid &= chain.ChainStatus.Length == 0;
            }


        }

        /// <summary>
        /// Get the specified resource contents
        /// </summary>
        public void Post<T>(String resourcePath, T data)
        {
            int retry = 0;
            while (retry++ < 3)
            {
                try
                {
                    Uri requestUri = new Uri(String.Format("{0}/{1}", this.m_baseUri, resourcePath));
                    WebRequest request = WebRequest.Create(requestUri);
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    if (!String.IsNullOrEmpty(s_username))
                        request.Headers.Add("Authorization", String.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", s_username, s_password)))));

                    var serializer = new DataContractJsonSerializer(typeof(T));
                    serializer.WriteObject(request.GetRequestStream(), data);
                    var response = request.GetResponse();
                    return;
                }
                catch (WebException e)
                {
                    if ((e.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        frmAuthenticate authForm = new frmAuthenticate();
                        if (authForm.ShowDialog() == DialogResult.OK)
                        {
                            s_username = authForm.Username;
                            s_password = authForm.Password;
                        }
                        else
                            throw new SecurityException("Authorization for service failed!");
                    }
                    else
                        throw;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            throw new SecurityException("Authorization for service failed!");
        }
        /// <summary>
        /// Get the specified resource contents
        /// </summary>
        public T Get<T>(String resourcePath, params KeyValuePair<String,Object>[] queryParms)
        {

            int retry = 0;

            while (retry++ < 3)
            {
                try
                {
                    StringBuilder queryBuilder = new StringBuilder();
                    if (queryParms != null)
                        foreach (var qp in queryParms)
                            queryBuilder.AppendFormat("{0}={1}&", qp.Key, qp.Value);
                    if (queryBuilder.Length > 0)
                        queryBuilder.Remove(queryBuilder.Length - 1, 1);
                    Uri requestUri = new Uri(String.Format("{0}/{1}?{2}", this.m_baseUri, resourcePath, queryBuilder));

                    WebRequest request = WebRequest.Create(requestUri);
                    request.Timeout = 600000;
                    request.Method = "GET";
                    if(!String.IsNullOrEmpty(s_username))
                        request.Headers.Add("Authorization", String.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", s_username, s_password)))));
                    var response = request.GetResponse();

                    var serializer = new DataContractJsonSerializer(typeof(T));
                    T retVal = (T)serializer.ReadObject(response.GetResponseStream());
                    //Thread.Sleep(100); // Yeah, you're reading that right... Idk why but GIIS WS don't like to be called too quickly
                    return retVal;
                }
                catch(WebException e)
                {
                    if ((e.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        frmAuthenticate authForm = new frmAuthenticate();
                        if (authForm.ShowDialog() == DialogResult.OK)
                        {
                            s_username = authForm.Username;
                            s_password = authForm.Password;
                        }
                        else
                            throw new SecurityException("Authorization for service failed!");
                    }
                    else
                        throw;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            throw new SecurityException("Authorization for service failed!");
        }

        /// <summary>
        /// Get raw unparsed response
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <param name="queryParms"></param>
        /// <returns></returns>
        public string GetRawResponse(String resourcePath, params KeyValuePair<String, Object>[] queryParms)
        {
            try
            {
                StringBuilder queryBuilder = new StringBuilder();
                if (queryParms != null)
                    foreach (var qp in queryParms)
                        queryBuilder.AppendFormat("{0}={1}&", qp.Key, qp.Value);
                if (queryBuilder.Length > 0)
                    queryBuilder.Remove(queryBuilder.Length - 1, 1);
                Uri requestUri = new Uri(String.Format("{0}/{1}?{2}", this.m_baseUri, resourcePath, queryBuilder));

                WebRequest request = WebRequest.Create(requestUri);
                request.Timeout = 600000;
                request.Method = "GET";
                var response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
