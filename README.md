**This library is a work in progress and will be force-pushed**

# Deribit.S4KTNET
Deribit Rest and Websocket library

* only supports v2
* only supports web sockets 
* does not support rest or FIX api

# Installation

# Usage

# Coverage

## Rest Api

| Api | Method | Covered | Notes |
| --- | ---- | ------- | ----- |
| Authentication | *   | ✗ | unsupported |
| SessionManagement | *   | ✗ | unsupported |
| Supporting | *   | ✗ | unsupported |
| Subscriptions | *   | ✗ | unsupported |
| AccountManagement | *   | ✗ | unsupported |
| Trading | *   | ✗ | unsupported |
| MarketData | *   | ✗ | unsupported |
| Wallet | *   | ✗ | unsupported |
| Notifications | *   | ✗ | unsupported |

## WebSocket Api

| Api | Method | Covered | Notes |
| --- | ------ | ------- | ----- |
| Authentication | [public/auth](https://docs.deribit.com/v2/#public-auth) | ✗ | in progress |
| Authentication | [private/logout](https://docs.deribit.com/v2/#private-logout) | ✗ | in progress |
| SessionManagement | [public/set_heartbeat](https://docs.deribit.com/v2/#session-management) | ✗ | in progress |
| SessionManagement | [public/disable_heartbeat](https://docs.deribit.com/v2/#public-disable_heartbeat) | ✗ | in progress |
| SessionManagement | [private/enable_cancel_on_disconnect](https://docs.deribit.com/v2/#private-enable_cancel_on_disconnect) | ✗ | in progress |
| SessionManagement | [private/disable_cancel_on_disconnect](https://docs.deribit.com/v2/#private-disable_cancel_on_disconnect) | ✗ | in progress |
| Supporting | [public/hello](https://docs.deribit.com/v2/#public-hello) | ✔ | |
| Supporting | [public/get_time](https://docs.deribit.com/v2/#public-get_time) | ✔ | |
| Supporting | [public/test](https://docs.deribit.com/v2/#public-test) | ✔ | |
| Subscriptions | [public/subscribe](https://docs.deribit.com/v2/#public-subscribe) | ✔ | |
| Subscriptions | [public/unsubscribe](https://docs.deribit.com/v2/#public-unsubscribe) | ✔ | |
| Subscriptions | [private/subscribe](https://docs.deribit.com/v2/#private-subscribe) | ✗ | in progress |
| Subscriptions | [private/unsubscribe](https://docs.deribit.com/v2/#private-unsubscribe) | ✗ | in progress |
| AccountManagement | [public/get_announcements](https://docs.deribit.com/v2/#public-get_announcements) | ✗ | |
| AccountManagement | private/change_subaccount_name | ✗ | |
| AccountManagement | private/create_subaccount | ✗ | |
| AccountManagement | private/diable_tfa_for_subaccount | ✗ | |
| AccountManagement | private/get_account_summary | ✗ | |
| AccountManagement | private/get_email_language | ✗ | |
| AccountManagement | private/get_new_announcements | ✗ | |
| AccountManagement | [private/get_position](https://docs.deribit.com/v2/#private-get_position) | ✗ | todo |
| AccountManagement | [private/get_positions](https://docs.deribit.com/v2/#private-get_positions) | ✗ | todo |
| AccountManagement | private/get_subaccounts | ✗ | |
| AccountManagement | private/set_announcement_as_read | ✗ | |
| AccountManagement | private/set_email_for_subaccount | ✗ | |
| AccountManagement | private/set_email_language | ✗ | |
| AccountManagement | private/set_password_for_subaccount | ✗ | |
| AccountManagement | private/toggle_notifications_from_subaccount | ✗ | |
| AccountManagement | private/toggle_subaccount_login | ✗ | |
| Trading | [private/buy](https://docs.deribit.com/v2/#private-buy) | ✗ | todo |
| Trading | [private/sell](https://docs.deribit.com/v2/#private-sell) | ✗ | todo |
| Trading | [private/cancel](https://docs.deribit.com/v2/#private-cancel) | ✗ | todo |
| Trading | [private/cancel_all](https://docs.deribit.com/v2/#private-cancel_all) | ✗ | todo |
| Trading | [private/cancel_all_by_currency](https://docs.deribit.com/v2/#private-cancel_all_by_currency) | ✗ | todo |
| Trading | [private/cancel_all_by_instrument](https://docs.deribit.com/v2/#private-cancel_all_by_instrument) | ✗ | todo |
| Trading | [private/close_position](https://docs.deribit.com/v2/#private-close_position) | ✗ | todo |
| Trading | [private/get_margins](https://docs.deribit.com/v2/#private-get_margins) | ✗ | todo |
| Trading | [private/get_open_orders_by_currency](https://docs.deribit.com/v2/#private-get_open_orders_by_currency) | ✗ | todo |
| Trading | [private/get_open_orders_by_instrument](https://docs.deribit.com/v2/#private-get_open_orders_by_instrument) | ✗ | todo |
| Trading | [private/get_order_history_by_currency](https://docs.deribit.com/v2/#private-get_order_history_by_currency) | ✗ | todo |
| Trading | [private/get_order_margin_by_ids](https://docs.deribit.com/v2/#private-get_order_margin_by_ids) | ✗ | todo |
| Trading | [private/get_order_state](https://docs.deribit.com/v2/#private-get_order_state) | ✗ | todo |
| Trading | [private/get_stop_order_history](https://docs.deribit.com/v2/#private-get_stop_order_history) | ✗ | todo |
| Trading | [private/get_user_trades_by_currency](https://docs.deribit.com/v2/#private-get_user_trades_by_currency) | ✗ | todo |
| Trading | [private/get_user_trades_by_currency_and_time](https://docs.deribit.com/v2/#private-get_user_trades_by_currency_and_time) | ✗ | todo |
| Trading | [private/get_user_trades_by_instrument](https://docs.deribit.com/v2/#private-get_user_trades_by_instrument) | ✗ | todo |
| Trading | [private/get_user_trades_by_instrument_and_time](https://docs.deribit.com/v2/#private-get_user_trades_by_instrument_and_time) | ✗ | todo |
| Trading | [private/get_user_trades_by_order](https://docs.deribit.com/v2/#private-get_user_trades_by_order) | ✗ | todo |
| Trading | [private/get_settlement_history_by_instrument](https://docs.deribit.com/v2/#private-get_settlement_history_by_instrument) | ✗ | |
| Trading | [private/get_settlement_history_by_currency](https://docs.deribit.com/v2/#private-get_settlement_history_by_currency) | ✗ | |
| MarketData | [public/get_book_summary_by_currency](https://docs.deribit.com/v2/#public-get_book_summary_by_currency) | ✗ | todo |
| MarketData | [public/get_book_summary_by_instrument](https://docs.deribit.com/v2/#public-get_book_summary_by_instrument) | ✗ | todo |
| MarketData | [public/get_contact_size](https://docs.deribit.com/v2/#public-get_contract_size) | ✗ | todo |
| MarketData | [public/get_currencies](https://docs.deribit.com/v2/#public-get_currencies) | ✗ | todo |
| MarketData | [public/get_funding_chart_data](https://docs.deribit.com/v2/#public-get_funding_chart_data) | ✗ | todo |
| MarketData | [public/get_historical_volatility](https://docs.deribit.com/v2/#public-get_historical_volatility) | ✗ | todo |
| MarketData | [public/get_index](https://docs.deribit.com/v2/#public-get_index) | ✗ | todo |
| MarketData | [public/get_instruments](https://docs.deribit.com/v2/#public-get_instruments) | ✗ | todo |
| MarketData | [public/get_last_settlemens_by_currency](https://docs.deribit.com/v2/#public-get_last_settlements_by_currency) | ✗ | todo |
| MarketData | [public/get_last_settlemens_by_instrument](https://docs.deribit.com/v2/#public-get_last_settlements_by_instrument) | ✗ | todo |
| MarketData | [public/get_last_trades_by_currency](https://docs.deribit.com/v2/#public-get_last_trades_by_currency) | ✗ | todo |
| MarketData | [public/get_last_trades_by_currency_and_time](https://docs.deribit.com/v2/#public-get_last_trades_by_currency_and_time) | ✗ | todo |
| MarketData | [public/get_last_trades_by_instrument](https://docs.deribit.com/v2/#public-get_last_trades_by_instrument) | ✗ | todo |
| MarketData | [public/get_last_trades_by_instrument_and_time](https://docs.deribit.com/v2/#public-get_last_trades_by_instrument_and_time) | ✗ | todo |
| MarketData | [public/get_order_book](https://docs.deribit.com/v2/#public-get_order_book) | ✗ | todo |
| MarketData | [public/get_trade_volumes](https://docs.deribit.com/v2/#public-get_trade_volumes) | ✗ | todo |
| MarketData | [public/get_tradingview_chart_data](https://docs.deribit.com/v2/#public-get_tradingview_chart_data) | ✗ | |
| MarketData | [public/ticker](https://docs.deribit.com/v2/#public-ticker) | ✗ | todo |
| Wallet | [private/cancel_transfer_by_id](https://docs.deribit.com/v2/#private-cancel_transfer_by_id) | ✗ | |
| Wallet | [private/cancel_withdrawal](https://docs.deribit.com/v2/#private-cancel_withdrawal) | ✗ | |
| Wallet | [private/create_deposit_address](https://docs.deribit.com/v2/#private-create_deposit_address) | ✗ | |
| Wallet | [private/get_current_deposit_address](https://docs.deribit.com/v2/#private-get_current_deposit_address) | ✗ | |
| Wallet | [private/get_deposits](https://docs.deribit.com/v2/#private-get_deposits) | ✗ | |
| Wallet | [private/get_transfers](https://docs.deribit.com/v2/#private-get_transfers) | ✗ | |
| Wallet | [private/get_withdrawals](https://docs.deribit.com/v2/#private-get_withdrawals) | ✗ | |
| Wallet | [private/withdraw](https://docs.deribit.com/v2/#private-withdraw) | ✗ | |
| Notifications | [announcements](https://docs.deribit.com/v2/#announcements) | ○ | partially tested |
| Notifications | [book.{instrument_name}.{group}.{depth}.{interval}](https://docs.deribit.com/v2/#book-instrument_name-group-depth-interval) | ○ | partially tested |
| Notifications | [book.{instrument_name}.{interval}](https://docs.deribit.com/v2/#book-instrument_name-interval) | ✔ |  |
| Notifications | [deribit_price_index.{index_name}](https://docs.deribit.com/v2/#deribit_price_index-index_name) | ✔ |  |
| Notifications | [deribit_price_ranking.{index_name}](https://docs.deribit.com/v2/#deribit_price_ranking-index_name) | ✗ | todo |
| Notifications | [estimated_expiration_price.{index_name}](https://docs.deribit.com/v2/#estimated_expiration_price-index_name) | ✗ | todo |
| Notifications | [markprice.options.{index_name}](https://docs.deribit.com/v2/#markprice-options-index_name) | ✗ | todo |
| Notifications | [perpetual.{instrument_name}.{interval}](https://docs.deribit.com/v2/#perpetual-instrument_name-interval) | ✗ | todo |
| Notifications | [quote.{instrument_name}](https://docs.deribit.com/v2/#quote-instrument_name) | ✔ |  |
| Notifications | [ticker.{instrument_name}.{interval}](https://docs.deribit.com/v2/#ticker-instrument_name-interval) | ✔ |  |
| Notifications | [trades.{instrument_name}.{interval}](https://docs.deribit.com/v2/#trades-instrument_name-interval) | ○ | partially tested |
| Notifications | [user.orders.{instrument_name}.{interval}](https://docs.deribit.com/v2/#user-orders-instrument_name-interval) | ✗ | todo |
| Notifications | [user.orders.{kind}.{currency}.{interval}](https://docs.deribit.com/v2/#user-orders-kind-currency-interval) | ✗ | todo |
| Notifications | [user.portfolio.{currency}](https://docs.deribit.com/v2/#user-portfolio-currency) | ✗ | todo |
| Notifications | [user.trades.{instrument_name}.{interval}](https://docs.deribit.com/v2/#user-trades-instrument_name-interval) | ✗ | todo |
| Notifications | [user.trades.{kind}.{currency}.{interval}](https://docs.deribit.com/v2/#user-trades-kind-currency-interval) | ✗ | todo |



# Security

* Do NOT trust the binaries with your keys
* compiling from source
* binary signatures