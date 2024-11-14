package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class CourtOwner(
    var id: String,
    var userId: String
) : Parcelable