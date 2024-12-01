package com.maxholmes.androidapp.data.model

import android.os.Parcelable
import kotlinx.parcelize.Parcelize

@Parcelize
data class User(
    var id: String,
    var code: Int,
    var username: String,
    var password: String,
    var name: String,
    var email: String?,
    var phoneNumber: String?,
    var isActive: Int,
    var avatarUrl: String?,
    var addressId: String,
    var roleId: String
) : Parcelable