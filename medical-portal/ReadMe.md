# Road Safety BC Driver Medical Fitness - Medical Practitioner Portal
[Repository](https://github.com/bcgov/rsbc-dmf/tree/main/medical-portal)

## DOCUMENTATION
[Confluence](https://jag.gov.bc.ca/wiki/display/DFTP/%5BProject+Base%5D+-+Practitioner+Portal+and+eDMER)
[Jira](https://jag.gov.bc.ca/jirarsi/secure/RapidBoard.jspa?rapidView=2503&projectKey=DFTDP)
[Wireframes](https://dmft.number41media.com/Wireframes/medical_portal/)

## PREREQUISITES
- Keycloak
- AWS S3
- OpenShift

## BUILD
- For all OS, you can use `dotnet build` from the root of the solution
- For Windows, you can use Visual Studio build
- For Mac, you can use JetBrains Rider build
- TODO add docker build steps to run "\Dockerfile" to build the docker image

## DEVELOP
- Visual Studio IDE 2022 (not tested with earlier versions)
- .NET 8.0 SDK

## RUN
Run solution "\medical-portal.sln" to run all projects needed for medical portal or run individual projects as needed
To run the frontend, cd to folder /medical-portal/src/UI and run `npm run start`

You will need to add the following in VS "Configure Startup Projects...":
- Rsbc.Dmf.CaseManagement.Service
- Pssg.Dmf.IcbcAdapter
- Pssg.DocumentStorageAdapter
- PidpAdapter.Service
- RSBC.DMF.MedicalPortal.API

## DEPLOY
[Github Action - Api CI](https://github.com/bcgov/rsbc-dmf/actions/workflows/ci-medical-portal-api.yml)
[Github Action - Api CD](https://github.com/bcgov/rsbc-dmf/actions/workflows/cd-medical-portal-api.yml)
[Github Action - UI CI](https://github.com/bcgov/rsbc-dmf/actions/workflows/ci-medical-portal-ui.yml)
[Github Action - UI CD](https://github.com/bcgov/rsbc-dmf/actions/workflows/cd-medical-portal-ui.yml)

## DEBUG
[Splunk](https://splunk.jag.gov.bc.ca/)

## TEST
- See project "\RSBC.Tests.Unit.DMF.MedicalPortal.API\RSBC.Tests.Unit.DMF.MedicalPortal.API.csproj" for unit tests
- [Swagger UI](http://localhost:5000/swagger/index.html)
- Postman see file "\DMFT.postman_collection.json"

## RESOURCES
[OpenShift Silver](https://oauth-openshift.apps.silver.devops.gov.bc.ca/oauth/authorize?client_id=console&redirect_uri=https%3A%2F%2Fconsole.apps.silver.devops.gov.bc.ca%2Fauth%2Fcallback&response_type=code&scope=user%3Afull&state=bd57c0b6)
[Keycloak](https://test.healthprovideridentityportal.gov.bc.ca/)
AWS S3
[Chefs](https://submit.digital.gov.bc.ca/app/)
[Chefs test eDMER form](https://submit.digital.gov.bc.ca/app/form/manage?f=5383fc89-b219-49a2-924c-251cd1557eb8)

## ONEHEALTH
The local, DEV, and TEST environments all use OneHealth for authentication and to load the endorsements with licences. 
The website is the same for all environments: test.healthprovideridentityportal.gov.bc.ca
Use this portal to add endorsements, enrol in DMFT, and to add licences

Use the PIDP0000X users, since the dmfw0000X users are not in a usable state and we have not heard back from OneHealth. Unfortunately, this means that the user data will be reset and does make testing difficult
To add a licence to a PIDP user, use any licence type and use licence number that matches X e.g. PIDP00001 would have licence 1
To enrol the user in DMFT, click on the access link on top menu, click on "Get Access" link on the page, click on "Driver Medical Fitness", and then click on the "Enrol" button

### How to add endorsements
1. Login as user A
2. Navigate to endorsements
3. Enter the email of user B and request endorsement
4. Logout
5. Navigate to [Mailhog](https://mailhog-test.healthprovideridentityportal.gov.bc.ca/)
6. Open new email from user A and click "this link" and follow the instructions in the email
7. Login as user B after clicking the link
8. At the top of the page, you will see an pending endorsement link, click on the link
9. Under incoming requests, you will see a request from user A, click Approve
10. Logout
11. Navigate to Mailhog
12. Open new email from system and click the link and follow the instructions in the email
13. Login as user A
14. Click on the pending endorsement request link
15. Under incoming requests, you will see an request from user B, click Approve
16. You will receive a new email and also see a new "Working relationship" for both users

### Contacts
Mailhot, Nicholas <Nick.Mailhot@nttdata.com> - Scrum Master
Sekhon, Khushwinder <Khushwinder.Sekhon@gov.bc.ca> - For updating the dmfw0000X users, which were suppose to be persisted for our testing
Morgan Wayling morgan.wayling@gov.bc.ca - Project manager? No response from Morgan either. In theory, would triage to contact Nick Mailhot

## TROUBLE SHOOTING
- If you find that gRPC client is not updating the models, delete the gRPC client and service bin and obj folders (bonus points for adding a project xml script to delete these folders on clean)

## HOW-TO

### How to claim a DMER
1. Login in as a practitioner
2. Search for a DMER case id e.g. "H8B2S1"
3. 

### Chefs IFrame origin
Currently, the IFrame origin only allows `var cors_origin = "https://dev.roadsafetybc.gov.bc.ca";`, see top code section of assets/chefs/initChefsForm.js.
If you want to test chefs form locally, edit the cors_origin to be `var cors_origin = "https://localhost:4020";`
In the future, we could consider replacing the iframe or doing something like this [StackOverflow](https://stackoverflow.com/questions/52002608/javascript-window-opener-postmessage-cross-origin-with-multiple-sub-domains)

### How to update chefs form scripts
Contact Shiv for admin access to chefs (MacFarlane, Shiv PSSG:EX)[<Shiv.MacFarlane@gov.bc.ca>]
1. In source control, update the files assets/chefs, to keep track of changes
2. Login to chefs, and select the eDMER form
3. Near the top of the form, there is a transparent control called "initChefsForm", edit the control
4. Click on the "Data" tab and expand the "Calculated Value" panel
5. Edit the "Javascript" textarea. You can copy the initChefsForm.js script and paste here.

### How to add a field to chefs form
1. Create a new version of the chefs form by clicking on the "+" button
2. Add an input field to chefs form, use camelCase for the field e.g. "firstName" on the "API" tab
3. Add a "Display" -> Label to the input field
4. If you need to style the input field, add a class key and unique value on the "API" tab -> Custom properties
5. Edit the assets/chefs/initChefsForm.js loadChefsBundleData method, update the const object and the "values" that matches the field name above e.g.
```
  const {
    patientCase: { driverLicenseNumber },
    driverInfo: { firstName, surname, birthDate, licenceClass },
    medicalConditions,
    dmerType
  } = fetchedBundleData;

  const values = {
    driverLicenseNumber,
    firstName,
    surname,
    birthDate,
    medicalConditions,
    dmerType,
    licenceClass
  };
```
6. Save and publish the chefs form