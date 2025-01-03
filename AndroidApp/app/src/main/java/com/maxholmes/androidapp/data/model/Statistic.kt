package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class Statistic(
    val courtCluster: CourtCluster,
    val price: Double,
    val bookingCount: Int
) : Parcelable
