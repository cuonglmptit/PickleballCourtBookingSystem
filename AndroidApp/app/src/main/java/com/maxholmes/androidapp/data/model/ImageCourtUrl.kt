package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class ImageCourtUrl(
    var id: String,
    var url: String,
    var courtClusterId: String
) : Parcelable