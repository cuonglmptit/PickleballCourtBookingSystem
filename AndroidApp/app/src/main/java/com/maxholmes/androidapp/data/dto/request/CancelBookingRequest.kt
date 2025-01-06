package com.maxholmes.androidapp.data.dto.request

data class CancelBookingRequest(
    val bookingId: String,
    val reason: String? = null
)