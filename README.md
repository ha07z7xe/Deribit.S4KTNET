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

| Operation | Path | Covered | Notes |
| --------- | ---- | ------- | ----- |
| -         | -    | -       | -     |

## WebSocket Api

| Type | Name | Method | Covered | Notes |
| ---  | --------- | ---- | ------- | ----- |
| Authentication | Authenticate | [public/auth](https://docs.deribit.com/v2/#public-auth) | ✗ | in progress |
| Authentication | Logout | [private/logout](https://docs.deribit.com/v2/#private-logout) | ✗ | in progress |
| SessionManagement | SetHeartbeat | [public/set_heartbeat](https://docs.deribit.com/v2/#session-management) | ✗ | in progress |
| SessionManagement | DisableHeartbeat | [public/disable_heartbeat](https://docs.deribit.com/v2/#public-disable_heartbeat) | ✗ | in progress |
| SessionManagement | EnableCancelOnDisconnect | [private/enable_cancel_on_disconnect](https://docs.deribit.com/v2/#private-enable_cancel_on_disconnect) | ✗ | in progress |
| SessionManagement | DisableCancelOnDisconnect | [private/disable_cancel_on_disconnect](https://docs.deribit.com/v2/#private-disable_cancel_on_disconnect) | ✗ | in progress |
| Supporting | Hello | [public/hello](https://docs.deribit.com/v2/#public-hello) | ✔ | |
| Supporting | Server Time | [public/get_time](https://docs.deribit.com/v2/#public-get_time) | ✔ | |
| Supporting | Test | [public/test](https://docs.deribit.com/v2/#public-test) | ✔ | |
| Subscriptions | Subscribe | [public/subscribe](https://docs.deribit.com/v2/#public-subscribe) | ✔ | |
| Subscriptions | Unsubscribe | [public/unsubscribe](https://docs.deribit.com/v2/#public-unsubscribe) | ✔ | |
| Subscriptions | Subscribe | [private/subscribe](https://docs.deribit.com/v2/#private-subscribe) | ✗ | in progress |
| Subscriptions | Unsubscribe | [private/unsubscribe](https://docs.deribit.com/v2/#private-unsubscribe) | ✗ | in progress |
| AccountManagement | Announcements | [public/get_announcements](https://docs.deribit.com/v2/#public-get_announcements) | ✗ | |
| AccountManagement | ChangeSubAccountName | private/change_subaccount_name | ✗ | |
| AccountManagement | CreateSubAccount | private/create_subaccount | ✗ | |
| AccountManagement | DisableTfaForSubAccount | private/diable_tfa_for_subaccount | ✗ | |
| AccountManagement | AccountSummary | private/get_account_summary | ✗ | |
| AccountManagement | EmailLanguage | private/get_email_language | ✗ | |
| AccountManagement | Announcements | private/get_new_announcements | ✗ | |
| AccountManagement | Position | [private/get_position](https://docs.deribit.com/v2/#private-get_position) | ✗ | todo |
| AccountManagement | Positions | [private/get_positions](https://docs.deribit.com/v2/#private-get_positions) | ✗ | todo |
| AccountManagement | SubAccounts | private/get_subaccounts | ✗ | |
| AccountManagement | MarkAnnouncementAsRead | private/set_announcement_as_read | ✗ | |
| AccountManagement | SetEmailForSubAccount | private/set_email_for_subaccount | ✗ | |
| AccountManagement | SetEmailLanguage | private/set_email_language | ✗ | |
| AccountManagement | SetPasswordForSubAccount | private/set_password_for_subaccount | ✗ | |
| AccountManagement | ToggleNotificationsFromSubAccount | private/toggle_notifications_from_subaccount | ✗ | |
| AccountManagement | ToggleSubAccountLogin | private/toggle_subaccount_login | ✗ | |
| Trading | Buy | [private/buy](https://docs.deribit.com/v2/#private-buy) | ✗ | todo |
| Trading | Sell | [private/sell](https://docs.deribit.com/v2/#private-sell) | ✗ | todo |
| Trading | Cancel | [private/cancel](https://docs.deribit.com/v2/#private-cancel) | ✗ | todo |
| Trading | CancelAll | [private/cancel_all](https://docs.deribit.com/v2/#private-cancel_all) | ✗ | todo |
| Trading | CancelAllByCurrency | [private/cancel_all_by_currency](https://docs.deribit.com/v2/#private-cancel_all_by_currency) | ✗ | todo |
| Trading | CancelAllByInstrument | [private/cancel_all_by_instrument](https://docs.deribit.com/v2/#private-cancel_all_by_instrument) | ✗ | todo |
| Trading | ClosePosition | [private/close_position](https://docs.deribit.com/v2/#private-close_position) | ✗ | todo |
| Trading | GetMargins | [private/get_margins](https://docs.deribit.com/v2/#private-get_margins) | ✗ | todo |
| Trading | GetOpenOrdersByCurrency | [private/get_open_orders_by_currency](https://docs.deribit.com/v2/#private-get_open_orders_by_currency) | ✗ | todo |
| Trading | GetOpenOrdersByInstrument | [private/get_open_orders_by_instrument](https://docs.deribit.com/v2/#private-get_open_orders_by_instrument) | ✗ | todo |
| Trading | GetOrderHistoryByCurrency | [private/get_order_history_by_currency](https://docs.deribit.com/v2/#private-get_order_history_by_currency) | ✗ | todo |
| Trading | GetOrderMarginByIds | [private/get_order_margin_by_ids](https://docs.deribit.com/v2/#private-get_order_margin_by_ids) | ✗ | todo |
| Trading | GetOrderState | [private/get_order_state](https://docs.deribit.com/v2/#private-get_order_state) | ✗ | todo |
| Trading | GetStopOrderHistory | [private/get_stop_order_history](https://docs.deribit.com/v2/#private-get_stop_order_history) | ✗ | todo |
| Trading | GetUserTradesByCurrency | [private/get_user_trades_by_currency](https://docs.deribit.com/v2/#private-get_user_trades_by_currency) | ✗ | todo |
| Trading | GetUserTradesByCurrencyAndTime | [private/get_user_trades_by_currency_and_time](https://docs.deribit.com/v2/#private-get_user_trades_by_currency_and_time) | ✗ | todo |
| Trading | GetUserTradesByInstrument | [private/get_user_trades_by_instrument](https://docs.deribit.com/v2/#private-get_user_trades_by_instrument) | ✗ | todo |
| Trading | GetUserTradesByInstrumentAndTime | [private/get_user_trades_by_instrument_and_time](https://docs.deribit.com/v2/#private-get_user_trades_by_instrument_and_time) | ✗ | todo |
| Trading | GetUserTradesByOrder | [private/get_user_trades_by_order](https://docs.deribit.com/v2/#private-get_user_trades_by_order) | ✗ | todo |
| Trading | GetSettlementHistoryByInstrument | [private/get_settlement_history_by_instrument](https://docs.deribit.com/v2/#private-get_settlement_history_by_instrument) | ✗ | |
| Trading | GetSettlementHistoryByCurrency | [private/get_settlement_history_by_currency](https://docs.deribit.com/v2/#private-get_settlement_history_by_currency) | ✗ | |

## WebSocket Notification

# Security

# 


https://docs.deribit.com/v2/#user-portfolio-currency

Security
=============
* Do NOT trust the binaries with your keys
* compiling from source
* binary signatures