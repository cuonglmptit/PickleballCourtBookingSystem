package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class Feedback(
    var id: String,
    var rating: Float,
    var comment: String?,
    var courtId: String,
    var bookingId: String,
    var customerId: String
) : Parcelable