package com.maxholmes.androidapp.utils.ext.authentication

import com.auth0.android.jwt.JWT

fun decodeJWT(token: String): Map<String, Any> {
    // Tạo đối tượng JWT từ token
    val jwt = JWT(token)

    // Trả về các thông tin trong payload (claims) của token dưới dạng Map
    val claims = mutableMapOf<String, Any>()

    // Lấy thông tin từ các claim
    claims["nameid"] = jwt.getClaim("nameid").asString() ?: "No nameid"
    claims["role"] = jwt.getClaim("role").asString() ?: "No role"
    claims["nbf"] = jwt.getClaim("nbf").asLong() ?: "No nbf"
    claims["exp"] = jwt.expiresAt ?: "No expiration"
    claims["iat"] = jwt.getClaim("iat").asLong() ?: "No issued at"

    // Trả về các claims dưới dạng Map
    return claims
}