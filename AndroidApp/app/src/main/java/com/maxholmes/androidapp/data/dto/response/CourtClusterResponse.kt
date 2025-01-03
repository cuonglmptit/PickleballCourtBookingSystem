package com.maxholmes.androidapp.data.dto.response

import android.os.Parcelable
import kotlinx.parcelize.Parcelize
import kotlin.time.Duration

@Parcelize
data class CourtClusterResponse(
    var id: String,
    var name: String,
    var openingTime: String,
    var closingTime: String,
    var description: String?,
    var addressId: String,
    var courtOwnerId: String,
    var imageUrl: String? = null
) : Parcelable
