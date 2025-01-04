package com.maxholmes.androidapp.data.dto.response

data class CustomerInfoDto(
    val id: String,
    val customerId: String,
    val name: String,
    val phoneNumber: String,
    val email: String,
    val avatarUrl: String
)