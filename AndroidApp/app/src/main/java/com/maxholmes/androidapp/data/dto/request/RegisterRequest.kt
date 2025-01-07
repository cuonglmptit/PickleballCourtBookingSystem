package com.maxholmes.androidapp.data.dto.request

import com.maxholmes.androidapp.utils.enum.RoleEnum

data class RegisterRequest(
    val username: String,
    val password: String,
    val confirmPassword: String,
    val name: String,
    val email: String,
    val phoneNumber: String,
    val role: RoleEnum
)
