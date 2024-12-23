package com.maxholmes.androidapp.data.dto.response

import com.google.gson.Gson
import com.google.gson.reflect.TypeToken

data class APIResponse(
    val success: Boolean,
    val statusCode: Int,
    val devMsg: String?,
    val userMsg: String?,
    val data: Any
)

inline fun <reified T> parseApiResponseData(data: Any?): T? {
    val gson = Gson()
    val json = gson.toJson(data)
    return gson.fromJson(json, object : TypeToken<T>() {}.type)
}