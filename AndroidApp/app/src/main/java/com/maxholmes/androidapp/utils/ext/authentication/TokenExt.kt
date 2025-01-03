package com.maxholmes.androidapp.utils.ext.authentication

import com.auth0.android.jwt.JWT

fun decodeJWT(token: String): Map<String, Any> {
    val jwt = JWT(token)

    val claims = mutableMapOf<String, Any>()

    claims["nameid"] = jwt.getClaim("nameid").asString() ?: "No nameid"
    claims["role"] = jwt.getClaim("role").asString() ?: "No role"
    claims["nbf"] = jwt.getClaim("nbf").asLong() ?: "No nbf"
    claims["exp"] = jwt.expiresAt ?: "No expiration"
    claims["iat"] = jwt.getClaim("iat").asLong() ?: "No issued at"

    return claims
}