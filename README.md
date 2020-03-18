# MNG Insurance Company

As a insurance company we've been asked to develop an application that manage some information about our insurance policies and company clients. To do that, we have two services that provide us with all the data needed:
* The list of company clients can be found at: 
http://www.mocky.io/v2/5808862710000087232b75ac
* The list of company policies can be found at:
http://www.mocky.io/v2/580891a4100000e8242b75c5

| Feature | Role |
| ------ | ------ |
| Get user data filtered by user id | admin, user |
| Get user data filterd by user nam | admin, user |
| Get the list of policies linked to a user name | admin |
| Get the user linked to a policy number | admin |

## Home Controller

* #### Status
  _Check API status._

  * **URL**
  _/status_

  * **Method:**
  `GET`

  *  **URL Params**
   _None_ 

  * **Success Response:**
    * **Code:** 200 OK
    **Content:** `{ status : "Server OK!" }`

* #### Token
  _Request a token for authorization and authentication of users._

  * **URL**
  _/token_

  * **Method:**
  `POST`
  
  *  **URL Params**
   **Required:**
    `name=[string]`

  * **Success Response:**
    * **Code:** 200 
    **Content:** 
    `{ token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhMGVjZTVkYi1jZDE0LTRmMjEtODEyZi05NjY2MzNlN2JlODYiLCJlbWFpbCI6ImJyaXRuZXlibGFua2Vuc2hpcEBxdW90ZXphcnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkJyaXRuZXkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImp0aSI6Ijc5YjkxOGMwLTY1ZDAtNDQ2ZC04OWFiLWFmM2Y3YTlmMTEwZCIsImV4cCI6MTU4NDI2MDg0MiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6ODA4MCIsImF1ZCI6Ik1ORyBJbnN1cmFuY2UgQ29tcGFueSJ9.l_skgBxTmsrGfiZhS7sainlBX245khFY5EwTP9qN4_U",
    expiration: "2020-03-15T08:27:22Z"
}`

  * **Error Response:**
    * **Code:** 404 Not Found
    **Content:** `{ error: "A client with name: Britnney was not found." }`

    OR
    * **Code:** 400 Bad Request
    **Content:** `{ error: "Parameters can't be null or empty." }`
  
  * **Sample Call:**
  ```javascript
    $.ajax({
      url: "/api/token",
      dataType: "json",
      data: { name: "Jhon Doe" }
      type : "POST",
      success : function(response) {
        console.log(response);
      }
    });
  ```
## Client Controller

* #### Get Client by ID

  Get client information searching by your ID.

  * **URL**
    _client/get-by-id/{id}_

  * **Method:**
  `GET`
  
  *  **URL Params**
    **Required:**
   `id=[string]`
    **Data Params**
    `headers: beare=[string]`

  * **Success Response:**
    * **Code:** 200 OK
    **Content:** `{ id: "a0ece5db-cd14-4f21-812f-966633e7be86", name: "Britney", email: "britneyblankenship@quotezart.com", role: "admin" }`
   
  * **Error Response:**
    * **Code:** 400 Not Found
  **Content**: `{ error: "A client with id: {id} was not found." }`

    OR

    * **Code:** 400 Bad Request
    **Content:** `{ error: "Parameters can't be null or empty." }`

  * **Sample Call:**
  ```javascript
    $.ajax({
      url: "/api/get-by-id/a0ece5db-cd14-4f21-812f-966633e7be86",
      dataType: "json",
      headers: { beare: token }
      type : "GET",
      success : function(response) {
        console.log(response);
      }
    });
  ```

* #### Get Client by Name
  Get client information searching by your Name.

  * **URL**
  _client/get-by-name/{name}_

  * **Method:**
  `GET`
  
  *  **URL Params**
    **Required:**
   `name=[string]`
    **Data Params**
    `headers: beare=[string]`

  * **Success Response:**
  
    * **Code:** 200 OK
    **Content:** `{ id: "a0ece5db-cd14-4f21-812f-966633e7be86", name: "Britney", email: "britneyblankenship@quotezart.com", role: "admin" }`
 
  * **Error Response:**
    * **Code:** 400 Not Found
    **Content:** `{ error: "A client with name: {name} was not found. }`

    OR

    * **Code:** 400 Bad Request
    **Content:** `{ error: "Parameters can't be null or empty." }`

  * **Sample Call:**
  ```javascript
    $.ajax({
      url: "/api/get-by-name/Britney",
      dataType: "json",
      headers: { beare: token }
      type : "GET",
      success : function(response) {
        console.log(response);
      }
    });
  ```
  
* #### Get Policies linked by Client Name
  Get policies linked to client searching by client name.

  * **URL**
  _client/get-policies-linked-by-name/{name}_

  * **Method:**
  `GET`
  
  *  **URL Params**
    **Required:**
   `name=[string]`
    **Data Params**
    `headers: beare=[string]`

  * **Success Response:**
      * **Code:** 200 OK
    **Content:** `[ { id: "7b624ed3-00d5-4c1b-9ab8-c265067ef58b", clientId: "a0ece5db-cd14-4f21-812f-966633e7be86", amountInsured: 399.89, email: "inesblankenship@quotezart.com", inceptionDate: "2015-07-06T06:55:49", installmentPayment: true }, { id: "6f514ec4-1726-4628-974d-20afe4da130c",
        clientId: "a0ece5db-cd14-4f21-812f-966633e7be86",
        amountInsured: 697.04,
        email: "inesblankenship@quotezart.com",
        inceptionDate: "2014-09-12T12:10:23",
        installmentPayment: false }, ... ]`
  * **Error Response:**
    * **Code:** 401 UNAUTHORIZED
 
    OR
    * **Code:** 404 Not Found
  **Content:** `{error: "The client has no linked policy."}`

    OR
    * **Code:** 400 Bad Request
    **Content:** `{ error: "Parameters can't be null or empty." }`

  * **Sample Call:**
  ```javascript
    $.ajax({
      url: "/api/get-policies-linked-by-name/Britney",
      dataType: "json",
      headers: { beare: token }
      type : "GET",
      success : function(response) {
        console.log(response);
      }
    });
  ```
  
* #### Get Policies linked by Client Name
  Get the user linked to a policy number ID.

  * **URL**
  _client/get-by-policy-id/{idPolity}_

  * **Method:**
  `GET`
  
  *  **URL Params**
    **Required:**
   `idPolity=[string]`
    **Data Params**
    `headers: beare=[string]`

  * **Success Response:**
    * **Code:** 200 OK
    **Content:** `{ id: "a0ece5db-cd14-4f21-812f-966633e7be86",name: "Britney", email: "britneyblankenship@quotezart.com", role:"admin"}`
   * **Error Response:**
      * **Code:** 401 UNAUTHORIZED
 
      OR
      * **Code:** 404 Not Found
  **Content:** `{ error: "A policy with id: {idPolicy} was not found."}`

     OR
      * **Code:** 400 Bad Request
    **Content:** `{ error: "Parameters can't be null or empty." }`

  * **Sample Call:**
  ```javascript
    $.ajax({
      url: "/api/get-by-policy-id/79c689f3-053a-459b-8c88-32a699817097",
      dataType: "json",
      headers: { beare: token }
      type : "GET",
      success : function(response) {
        console.log(response);
      }
    });
  ```

## License

MIT
