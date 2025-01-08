package com.maxholmes.androidapp.data.dto.request

data class AddCourtClusterRequest(
    val name: String,
    val description: String,
    val openingTime: String,
    val closingTime: String,
    val city: String,
    val district: String,
    val ward: String,
    val street: String,
    val numberOfCourts: Int,
    val image: String?
)
