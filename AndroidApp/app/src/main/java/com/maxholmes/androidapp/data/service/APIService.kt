package com.maxholmes.androidapp.data.service

import com.maxholmes.androidapp.data.dto.request.AddBookingRequest
import com.maxholmes.androidapp.data.dto.request.AddCourtClusterRequest
import com.maxholmes.androidapp.data.dto.request.CancelBookingRequest
import com.maxholmes.androidapp.data.dto.request.ConfirmBookingRequest
import com.maxholmes.androidapp.data.dto.request.LoginRequest
import com.maxholmes.androidapp.data.dto.request.RegisterRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.LoginResponse
import retrofit2.http.GET
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query
import java.util.Date
import java.util.UUID

interface APIService {

    @POST("api/Auth/Login")
    fun login(@Body loginRequest: LoginRequest): Call<LoginResponse>

    @GET("api/CourtCluster")
    fun getAllCourtClusters(): Call<APIResponse>

    @GET("api/CourtCluster/{id}")
    fun getCourtClusterById(@Path("id") id: String): Call<APIResponse>

    @GET("api/CourtCluster/Owner")
    fun getCourtClustersByOwner(@Header("Authorization") token: String): Call<APIResponse>

    @GET("api/CourtCluster/{id}/Courts")
    fun getCourtsByCourtClusterId(@Path("id") courtClusterId: String): Call<APIResponse>

    @GET("api/CourtTimeSlot/getAvailableSlots")
    fun getCourtTimeSlotsForBooking(@Query("courtId") courtId: String, @Query("date") date: String): Call<APIResponse>

    @POST("api/Auth/Register")
    fun register(@Body registerRequest: RegisterRequest): Call<APIResponse>

    @GET("/api/CourtTimeSlot/getAvailableSlots")
    fun getAvailableTimeSlots(@Query("courtId") courtId: String, @Query("date") date: String): Call<APIResponse>

    @GET("/api/Address/{id}")
    fun getAddressById(@Path("id") id: String): Call<APIResponse>

    @GET("/api/User/CourtOwner/GetInfo/{id}")
    fun getCourtOwnerInfo(@Path("id") id: String, @Header("Authorization") token: String): Call<APIResponse>

    @GET("/api/CourtCluster/Active")
    fun getActiveCourtClusters(): Call<APIResponse>

    @GET("/api/CourtCluster/{id}/Image")
    fun getImagesByCourtClusterId(@Path("id") courtClusterId: String): Call<APIResponse>

    @GET("/api/User/User/getInfo")
    fun getUserInfo(@Header("Authorization") token: String): Call<APIResponse>

    @POST("/api/Booking/add-booking")
    fun addBooking(@Body addBookingRequest: AddBookingRequest, @Header("Authorization") token: String): Call<APIResponse>

    @GET("/api/Booking/GetBookingSchedule")
    fun getBookingByUser(@Header("Authorization") token: String): Call<APIResponse>

    @POST("/api/Booking/cancel-booking")
    fun cancelBooking(@Body cancelBookingRequest: CancelBookingRequest, @Header("Authorization") token: String): Call<APIResponse>

    @GET("/api/CourtCluster/SearchCourtClusterWithFilter")
    fun searchCourtClusters(@Query("courtClusterName") name: String, @Query("cityName") city: String, @Query("date") date: String, @Query("startTime") startTime: String, @Query("endTime") endTime: String): Call<APIResponse>

    @POST("/api/CourtCluster")
    fun addCourtCluster(@Body addCourtClusterRequest: AddCourtClusterRequest, @Header("Authorization") token: String): Call<APIResponse>

    @POST("/api/Booking/court-owner-confirm-booking")
    fun courtOwnerConfirmBooking(@Body confirmBookingRequest: ConfirmBookingRequest, @Header("Authorization") token: String): Call<APIResponse>

    @POST("/api/Booking/court-owner-confirm-paid")
    fun courtOwnerConfirmPaid(@Body confirmBookingRequest: ConfirmBookingRequest, @Header("Authorization") token: String): Call<APIResponse>

    @GET("/api/booking/Statistic")
    fun getStatistic(@Header("Authorization") token: String, @Query("startDate") startDate: String, @Query("endDate") endDate: String): Call<APIResponse>

    @GET("/api/CourtCluster/Owner")
    fun getCourtClusterForOwner(@Header("Authorization") token: String): Call<APIResponse>

    @GET("/api/ImageCourtUrl/Cluster/{id}")
    fun getImagesByClusterId(@Path("id") clusterId: String): Call<APIResponse>
}