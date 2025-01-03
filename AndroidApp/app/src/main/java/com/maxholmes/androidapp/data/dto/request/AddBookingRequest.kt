package com.maxholmes.androidapp.data.dto.request

import com.maxholmes.androidapp.data.model.CourtTimeSlot

class AddBookingRequest (
    val courtTimeSlot: List<CourtTimeSlot>,
    val courtId: String
)