package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import java.time.LocalDateTime

@Parcelize
data class Booking(
    var id: String,
    var timeBooking: LocalDateTime,
    var amount: Double,
    var paymentStatus: Int,
    var courtId: String,
    var customerId: String
) : Parcelable