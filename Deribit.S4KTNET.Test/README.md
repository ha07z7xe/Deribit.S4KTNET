# Running the integration tests

The integration tests include both public and authenticated tests. These run against the Deribit testnet by default.

To successfully run the authenticated tests:
* Create a testnet account at [https://test.deribit.com](https://test.deribit.com)
* Create a file `config/secrets.json` (just copy `config/secrets.json.template`)
* Provide your account secrets in `secrets.json`. The template secrets are fake.
* Ensure your account is funded sufficiently.