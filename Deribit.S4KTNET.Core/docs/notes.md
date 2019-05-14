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
* Automapper
* Serilog
* FluentValidation

todo
======================
* check Dispose methods are called on DeribitService disposal


Subscriptions
==========================
* accept JToken
* deserialize to strongly typed object based off channel type
* Create dtos, and model types
* emit both events and observables from SubscriptionManagementService
