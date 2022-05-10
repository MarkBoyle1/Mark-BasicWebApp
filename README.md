# Mark-BasicWebApp

## Description

An API created for Phase Two of Acceleration. The API stores and retrieves a list 
of people. Each person has one property which is a 'Name'. People can be updated, added to 
and deleted from the mock database.

To update, add or delete a person, the '/person' endpoint needs to be used.
When the '/person' endpoint is used any response data returned in the body
will be in JSON format.

An example JSON response:

{"Id":1,"Name":"Mark"}

The '/greeting' endpoint will return a greeting as a string. The greeting can
be for all the people in the database or for a specific person. The greeting 
contains the time and date as well. 

An example greeting:

"Hello Mark, the time on the server is 11:30:14 am Wednesday, 23 February 2022"

## Base URL

mark-basic-web-app.svc.platform.myobdev.com

## Endpoints

### GET /person

Returns data for all stored persons in JSON format.

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     | [{"id": 1, "Name": "Mark"}, {"id": 2, "Name": "John"}] |

### POST /person

Adds a new person. Returns the id of the new person.

**Body Required**

Example body:

{"Name": "John"}

**Possible Respones**

| Status Code     | Response Body |
| ----------- | ----------- |
| 201     | 1 |
| 400   | "Error: Request body is empty."      |
| 400   | "Error: Invalid request body."     |

### GET /person/{id}

Returns data for an individual person in JSON format.

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     | {"id": 1, "Name": "John"} |
| 400   | "Error: No id was given."      |
| 404   | "Error: Id does not exist."     |

### DELETE /person/{id}

Deletes an individual person.

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     |  |
| 400   | "Error: No id was given."      |
| 404   | "Error: Id does not exist."      |

### PUT /person/{id}

Updates an individual person with new data.

**Body required**

Example body:

{"Name": "Andy"}

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     |   |
| 400   | "Error: No id was given."      |
| 404   | "Error: Id does not exist."      |
| 400   | "Error: Invalid request body."     |
| 400   | "Error: Request body is empty."    |

### GET /greeting

Returns a greeting with all persons.

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     | "Hello \<All names in database\>, the time on the server is \<time\> \<day\>, \<date\>" |

### GET /greeting/{id}

Returns a greeting for an individual person.

**Possible Responses**

| Status Code     | Response Body |
| ----------- | ----------- |
| 200     | "Hello \<name\>, the time on the server is \<time\> \<day\>, \<date\>" |
| 404   | "Error: Id does not exist."      |
