Features
=====================
* dont support HTTP. websocket only


Design
====================
* Split by public/private
* private requires authentication
* interface segregation - endpoint groups
* Rx.Net for subscriptions
* multiple authentication workflows
    * oath access token (required)
    * username/password
    * client_credentials
    * client_signature
    * + refresh tokens