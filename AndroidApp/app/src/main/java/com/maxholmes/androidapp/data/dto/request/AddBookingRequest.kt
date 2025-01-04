package com.maxholmes.androidapp.data.dto.request

import com.maxholmes.androidapp.data.model.CourtTimeSlot

class AddBookingRequest (
    val courtTimeSlotsIds: List<String>,
    val courtId: String
)