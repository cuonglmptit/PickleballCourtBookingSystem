package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class Court(
    var id: String,
    var courtNumber: Int,
    var description: String?,
    var courtClusterId: String
) : Parcelable