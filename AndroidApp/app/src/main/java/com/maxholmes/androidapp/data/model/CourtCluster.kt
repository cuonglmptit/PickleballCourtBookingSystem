package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import java.time.LocalTime

@Parcelize
data class CourtCluster(
    var id: String,
    var name: String,
    var openingTime: LocalTime,
    var closeingTime: LocalTime,
    var description: String?,
    var addressId: String,
    var courtOwnerId: String,
    var imageUrl: String
) : Parcelable
