package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import java.time.LocalTime

@Parcelize
data class CourtPrice(
    var id: String,
    var time: LocalTime,
    var price: Double,
    var courtClusterId: String
) : Parcelable