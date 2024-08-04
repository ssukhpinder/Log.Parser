# Log.Parser
A quick analysis of server logs and displays in tabular format is needed.

# Problem
Create a Frontend on a choice of your technology framework to display the following information (You may also create an output file):

## Task 1
Number of accesses to web server per host; sorted, host with most entries is top. The format is similar to the input format (space-separated columns).

### Expected Output
```
wpbfl2-45.gate.net 232
ix-mia5-17.ix.netcom.com 25
tanuki.twics.com 120
```

## Task 2
Number of successful resource accesses by URI. Only “GET” accesses to each URI are to be counted. Only requests which resulted in the HTTP reply code 200 (“OK”) are to be counted.

### Expected Output
```
/logos/small_ftp.gif 23
/icons/people.gif 12
```

### Sample File
[Epa-http.txt](https://github.com/ssukhpinder/Log.Parser/blob/main/Log.Parser.Server/epa-http.txt)

### Bonus
Allow a choice of time span for conversion.

# Prerequisites
- Visual Studio 2022 Latest Version
- Install Node
- Install angular packages using npm

# Solution
The technology stack used to solve the above problem is as follows
- .Net 8 Web API Backend with Swagger Enabled
- Angular version (17.3.8) with Bootstrap

# Project Breakdown
| Component          | Description                      |
| ------------------ | -------------------------------- |
| log.parser.client  | Angular Code                     |
| Log.Parser.Server  | REST API .NET 8 Code             |
| Log.Parser.BL      | Complete business logic          |
| Log.Parser.Tests   | Test Cases of the REST API Project|

# How to Run the Project
### Step 1: Clone the following repository using the Github CLI command
```
gh repo clone ssukhpinder/Log.Parser
```

### Step 2: Open the project in Visual Studio
Open the project in Visual Studio, then build the solution by right-clicking on the solution and selecting "Build Solution."

### Step 3: Setup Multi-Project Startup
Open the configure startup project dialogue project and make sure the following project actions as "Start"

- log.parser.client
- Log.Parser.Server

![image](https://github.com/user-attachments/assets/18ab74c8-225c-4916-9af3-9d23b35e678a)

### Step 4: Click Start
After successfully building, it should open two applications, Swagger UI and Angular app on the browser, as demonstrated below.

![run](https://github.com/user-attachments/assets/0a0ced24-182b-46cd-a33d-721904e3ff33)

# Other Configurations
The REST APIs are protected by CORS policy wherein only certain domain clients can access the APIs if the Angular application is unable to communicate with the .Net backend or running on a port other than 4200.

Then update the following code accordingly [here](https://github.com/ssukhpinder/Log.Parser/blob/main/Log.Parser.Server/Program.cs)

```
// Apply CORS policy so that APIs are accessible only to the Angular app domain i.e. https://localhost:4200
// To make it visible for other apps just provide domain names comma-separated or add them to the appsettings.json file

builder.Services.UseCorsPolicyHandler(new string[] { "https://localhost:4200" });
```

## Parsing Log file using Log Parser
> As the data inside the log file is available ONLY for the 29 or 30th of August and the Default value of the year is the current year i.e. 2024

- Choose the log file
- Select start date and time [Data available for 29th or 30th August 2024]
- Select end date and time [Data available for 29th or 30th August 2024]
- Click submit

### Testing Problem 1 using Angular App
Follow the instructions from Steps 1-4 defined above and then test the Angular UI as follows
![task1](https://github.com/user-attachments/assets/0fd91a19-908f-4421-840d-f3baee764b90)

### Testing Problem 2 using Angular App
Click the "Success Per Uri" tab in the navigation and similarly follow the instructions as follows
![task2](https://github.com/user-attachments/assets/c9b4e467-5994-420c-b57a-533afc866e69)


# Test Project
The parser project also contains test cases written using the xUnit Framework.

There is a total of 6 test cases as follows
- TC1: Performing problem task 1 defined above with a single log file entry
- TC2: Performing problem task 1 defined above with multiple log file entries
- TC3: Performing problem task 2 defined above with a single log file entry
- TC4: Performing problem task 2 defined above with multiple log file entries
- TC5: Performing problem task 1 with invalid content in the log file
- TC6: Performing problem task 2 with invalid content in the log file

### How to run Test Cases
Open Visual Studio > Run All Test Cases or follow the instructions below

![testcase](https://github.com/user-attachments/assets/b10666ff-ea82-4f11-b5e0-292e6370a526)


