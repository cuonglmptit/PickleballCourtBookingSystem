package com.maxholmes.androidapp.data.dto.response

data class UserInfoDto(
    val id: String,
    val name: String,
    val phoneNumber: String,
    val email: String,
    val avatarUrl: String?
)
