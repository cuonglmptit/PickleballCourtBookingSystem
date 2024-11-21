package com.maxholmes.androidapp.data.repository.remote

import com.google.gson.GsonBuilder
import com.maxholmes.androidapp.data.model.CourtCluster
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.Call
import retrofit2.http.Path

interface ApiService {

    @GET("api/example")
    fun getExample(): Call<CourtCluster>


    @GET("posts/{id}")
    fun getCourtClusterById(@Path("id") courtClusterId: Int): Call<CourtCluster>
}