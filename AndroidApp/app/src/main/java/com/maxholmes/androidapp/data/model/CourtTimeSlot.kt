package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import java.time.LocalDate
import java.time.LocalTime

@Parcelize
data class CourtTimeSlot(
    var id: String,
    var date: String,
    var time: String,
    var isAvailable: Int,
    var price: Double,
    var courtId: String
) : Parcelable