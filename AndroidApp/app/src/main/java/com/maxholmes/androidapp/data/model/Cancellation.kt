package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import java.time.LocalDateTime

@Parcelize
data class Cancellation(
    var id: String,
    var timeCancel: LocalDateTime,
    var bookingId: String,
    var reason: String
) : Parcelable