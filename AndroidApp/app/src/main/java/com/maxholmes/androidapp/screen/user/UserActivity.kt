package com.maxholmes.androidapp.screen.user

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.UserInfoDto
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityUserBinding
import com.maxholmes.androidapp.screen.authentication.LoginActivity
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.bookschedule.BookScheduleActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.customer.search.SearchActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.getRoleFromToken
import com.maxholmes.androidapp.utils.ext.loadImageCircleWithUrl
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class UserActivity : AppCompatActivity() {

    private lateinit var binding: ActivityUserBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityUserBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val token = SharedPreferencesUtils.getToken(this)
        if (token.isNullOrEmpty()) {
            navigateToLogin()
            return
        }

        fetchUserInfo(token)
        setupLogoutButton()
        binding.bottomNavigationView.selectedItemId = R.id.user
        setupBottomNavigation(token)
    }

    private fun fetchUserInfo(token: String) {
        val token = SharedPreferencesUtils.getToken(this) ?: return
        val headerAuthorize = "Bearer $token"
        RetrofitClient.ApiClient.apiService.getUserInfo(headerAuthorize).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    response.body()?.let { apiResponse ->
                        val userInfo: UserInfoDto? = parseApiResponseData(apiResponse.data)
                        userInfo?.let { updateUI(it, token) }
                    }
                } else {
                    Toast.makeText(this@UserActivity, "Lỗi khi lấy thông tin người dùng", Toast.LENGTH_SHORT).show()
                    Log.e("UserActivity", "Error: ${response.message()}")
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                Toast.makeText(this@UserActivity, "Không thể kết nối đến server", Toast.LENGTH_SHORT).show()
                Log.e("UserActivity", "Failure: ${t.message}")
            }
        })
    }

    private fun updateUI(userInfo: UserInfoDto, token: String) {
        binding.textViewName.text = userInfo.name
        binding.textViewEmail.text = userInfo.email
        binding.textViewPhoneNumber.text = userInfo.phoneNumber
        when (getRoleFromToken(token)) {
            "Customer" -> binding.tvUserRole.text = "Khách hàng"
            "CourtOwner" -> binding.tvUserRole.text = "Chủ sân"
            "Admin" -> binding.tvUserRole.text = "Quản trị viên"
            else -> binding.tvUserRole.text = "Khách hàng"
        }

        userInfo.avatarUrl?.let { url ->
            binding.imgAvatar.loadImageCircleWithUrl(url, R.drawable.ic_avatar)
        }
    }

    private fun setupLogoutButton() {
        binding.btnLogout.setOnClickListener {
            SharedPreferencesUtils.clearToken(this)
            navigateToLogin()
        }
    }

    private fun navigateToLogin() {
        val intent = Intent(this, LoginActivity::class.java)
        intent.flags = Intent.FLAG_ACTIVITY_NEW_TASK or Intent.FLAG_ACTIVITY_CLEAR_TASK
        startActivity(intent)
        finish()
    }

    private fun setupBottomNavigation(token: String) {
        binding.bottomNavigationView.setOnItemSelectedListener { item ->
            when (item.itemId) {
                R.id.home -> {
                    val intent = Intent()
                    if(getRoleFromToken(token) == "Customer") {
                        intent.setClass(this, HomeCustomerActivity::class.java)
                    } else if(getRoleFromToken(token) == "CourtOwner") {
                        intent.setClass(this, HomeCourtOwnerActivity::class.java)
                    }

                    startActivity(intent)
                    true
                }

                R.id.booking -> {
                    val intent = Intent()
                    if(getRoleFromToken(token) == "Customer") {
                        intent.setClass(this, BookScheduleActivity::class.java)
                    } else if(getRoleFromToken(token) == "CourtOwner") {
                        intent.setClass(this, BookScheduleActivity::class.java)
                    }
                    startActivity(intent)
                    true
                }

                R.id.search -> {
                    val intent = Intent(this, SearchActivity::class.java)
                    startActivity(intent)
                    true
                }

                R.id.user -> {
                    true
                }

                else -> false
            }
        }
    }
}
