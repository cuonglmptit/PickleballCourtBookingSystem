package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class CourtTimeBooking(
    var id: String,
    var bookingId: String,
    var courtTimeSlotId: String
) : Parcelable