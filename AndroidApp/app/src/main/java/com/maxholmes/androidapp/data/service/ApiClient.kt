package com.maxholmes.androidapp.data.service

import com.google.gson.GsonBuilder
import com.maxholmes.androidapp.utils.base.Constant
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object RetrofitClient {
    val gson = GsonBuilder()
        .setDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSXXX")
        .create()

    val retrofit : Retrofit by lazy {
        Retrofit.Builder()
            .baseUrl(Constant.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
    }

    object ApiClient {
        val apiService: APIService by lazy {
            retrofit.create(APIService::class.java)
        }
    }
}