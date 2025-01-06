package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import com.maxholmes.androidapp.utils.enum.BookingStatusEnum
import com.maxholmes.androidapp.utils.enum.PaymentStatusEnum
import kotlinx.parcelize.Parcelize
import java.time.LocalDateTime

@Parcelize
data class Booking(
    var id: String,
    var timeBooking: String,
    var status: BookingStatusEnum,
    var amount: Double,
    var paymentStatus: PaymentStatusEnum,
    var courtId: String,
    var courtClusterId: String,
    var customerId: String
) : Parcelable
