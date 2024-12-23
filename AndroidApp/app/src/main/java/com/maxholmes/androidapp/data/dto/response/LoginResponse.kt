package com.maxholmes.androidapp.data.dto.response

data class LoginResponse(
    val success: Boolean,
    val devMsg: String,
    val userMsg: String,
    val data: TokenData,
    val statusCode: Int
)

data class TokenData(
    val token: String
)