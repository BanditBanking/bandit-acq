# Errors

Errors described in the following section follows the RFC 7807 recommandations https://www.rfc-editor.org/rfc/rfc7807

## Ghost

| Property | Value |
| ------ | ----------- |
| **Error Code**   | glowfish |
| **Http Code** | 500 |
| **Title**    | An unknown exception occured |
| **Root Causes**    | - Possibly any exception that was not planned |
| **Related Endpoints**    | - all |

### Resolutions

- Check the service logs inside portainer
- Run the service in debug

## Harbor

| Property | Value |
| ------ | ----------- |
| **Error Code**   | harbor |
| **Http Code** | 503 |
| **Title**    | Bank server unreachable |
| **Root Causes**    | - Possible misconfiguration of banknet<br/>- The related bank server is down |
| **Related Endpoints**    | - all |

### Resolutions

- Ensure the correct server was contacted (ip:port)
- Ensure the banknet config is correct
- Verifiy that the specified server is running

## Salamander

| Property | Value |
| ------ | ----------- |
| **Error Code**   | salamander |
| **Http Code** | 404 |
| **Title**    | Bank not found |
| **Root Causes**    | - The bank id provided was incorrect<br/>- The banknet config file was not loaded properly |
| **Related Endpoints**    | - /auth/login/2fa/otp |

### Resolutions

- Ensure that the bank id is correct
- Check if the /config/banknet.json file is correct
- Ensure that it has been loaded by the acq, should normally warn in console if not found
