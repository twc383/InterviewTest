FundsLibrary Interview Test
===========================

Welcome to the FundsLibrary technical test!

This test is intended to be a simple test of coding ability. You are welcome to spend as much or as little time on it as you want.

## Test Details

This repository contains a skeleton 2 tier application containing an MVC web front-end and a back-end WebApi service. The purpose of the application is to display details of fund managers. Currently a simple overview page for fund managers and a details page are available.

The current challenge is to integrate this application with an external web service hosted at [https://www.fundslibrary.co.uk/FundsLibrary.DataApi.WebApi/](https://www.fundslibrary.co.uk/FundsLibrary.DataApi.WebApi/).

## The External API

The external API is an OData endpoint that allows you to search for securities (mutual funds, shares, etc). The API allows you to retrieve all securities, or to search for specific securities and is documented at: [https://www.fundslibrary.co.uk/FundsLibrary.DataApi.Web/](https://www.fundslibrary.co.uk/FundsLibrary.DataApi.Web/).

To connect you will need to supply an API key as an HTTP header in the request. You can use the API key "ZNMI5P30EXUXV1ULNEAQ" for the purposes of the test. The key should be sent in the Authorization header as a bearer token as follows:

    Authorization: Bearer ZNMI5P30EXUXV1ULNEAQ

## The Challenge

The aim of the challenge is to display funds that are managed by a given manager when viewing that managers details. To do this you will need to combine the results returned by the WebApi service in this solution with the results of the external service.

The JSON returned by the external service is a JSON array containing fund details. For each fund there is a StaticData.Management array containing a list of manager details as below:    

	{
		"Id": "a8a2c0f4-9fd0-43df-8953-181ab9bbfa7c",
		"StaticData": {
			"Management": {
				"Team": [
					{
						"Id": "bf9e5061-37d2-49f1-9b9b-212c94a3e3ae",
						"Name": "Sanjiv Duggal",
						"Photo": null,
						"PositionWithinTeam": null,
						"StartDate": "2015-10-01T00:00:00Z",
						"Role": "Secondary",
						"Bio": "tbc",
						"Location": "tbc"
					}
				]
			}
		}
	}

These details can be used to match up the managers that are stored in this application with fund details returned by the external API.

As a minimum you should aim to display a list of funds managed by each manager with the StaticData.Identification.IsinCode and the StaticData.Identification.FullName.

## How to submit your answer

The preferred option is for you to fork this repository & then submit a pull request once you have completed your changes.

Alternatively if you don't have (or want) a GitHub account you can download this repository [https://github.com/FundsLibrary/InterviewTest/archive/master.zip](https://github.com/FundsLibrary/InterviewTest/archive/master.zip) and put the results somewhere public on the web (Dropbox, public Google Drive folder, public One Drive folder). Please note we cannot accept results via email, or FTP links.

## Tooling

You can download the community edition of Visual Studio from: [http://www.visualstudio.com/en-us/visual-studio-community-vs.aspx](http://www.visualstudio.com/en-us/visual-studio-community-vs.aspx)
You can also complete this test using your favourite text editor and compile using csc.exe on Windows, or Mono [http://www.mono-project.com/](http://www.mono-project.com/) on other operating systems.
