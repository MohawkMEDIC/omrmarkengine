# omrmarkengine

This project is designed to allow easy creation of OMR (Optical Mark Recognition) templates and provides a bulk scanner which can be used for processing large amounts of images from a tray fed scanner.

The project is made up for three components:

## Core Processing Assembly: 
Allows easy integration of the OMR engine into your application

## Template Designer
Allows the creation of OMR form templates which are used for detecting answers from scanned images

## Bulk Scanner
An application which will bulk read data from a scanner and save the results to a formatted XML file. It also provides extension points for writing handlers for processed images.

# Features

* Form Designer – Allows easy creation of form scanning templates
* Batch Scanner – Can scan multiple documents from a sheet-fed scanner
* Works with WIA compliant scanners
* Auto Correction of Images
* Size adjustment (i.e. design the form with letter sized paper and scan in A4)
* Rotation adjustment (i.e. fix crooked scans)
* Color Adjustment (i.e. gets rid of light dirt and other artifacts from scanned image)
* Save output CSV / XML
