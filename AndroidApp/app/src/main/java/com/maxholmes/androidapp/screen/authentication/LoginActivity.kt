package com.maxholmes.androidapp.screen.authentication

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.LoginRequest
import com.maxholmes.androidapp.data.dto.response.LoginResponse
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityLoginBinding
import com.maxholmes.androidapp.screen.admin.HomeAdminActivity
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.test.TestActivity
import com.maxholmes.androidapp.screen.register.RegisterActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.authentication.decodeJWT
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class LoginActivity : AppCompatActivity() {

    private lateinit var binding: ActivityLoginBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.loginButton.setOnClickListener {
            val email = binding.email.text.toString().trim()
            val password = binding.password.text.toString().trim()

            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Bạn cần nhập đầy đủ tài khoản và mật khẩu!", Toast.LENGTH_SHORT).show()
            } else {
                login(email, password)
            }
        }

        binding.signupButton.setOnClickListener {
            val intent = Intent(this, RegisterActivity::class.java)
            startActivity(intent)
        }

    }


    private fun login(email: String, password: String) {
        binding.progressBar.visibility = View.VISIBLE

        val loginRequest = LoginRequest(username = email, password = password)

        RetrofitClient.ApiClient.apiService.login(loginRequest).enqueue(object : Callback<LoginResponse> {
            override fun onResponse(call: Call<LoginResponse>, response: Response<LoginResponse>) {
                binding.progressBar.visibility = View.GONE

                if (response.isSuccessful && response.body()?.success == true) {
                    val token = response.body()?.data?.token
                    Toast.makeText(this@LoginActivity, "Đăng nhập thành công", Toast.LENGTH_SHORT).show()

                    if (token != null) {
                        SharedPreferencesUtils.saveToken(this@LoginActivity, token)
                        val claims = decodeJWT(token)
                        val role = claims["role"] as String


                        val nextActivity = when (role) {
                            "Customer" -> HomeCustomerActivity::class.java
                            "CourtOwner" -> HomeCourtOwnerActivity::class.java
                            "Admin" -> HomeAdminActivity::class.java
                            else -> {
                                val loginIntent = Intent(this@LoginActivity, LoginActivity::class.java)
                                startActivity(loginIntent)
                                finish()
                                return
                            }
                        }
                        val intent = Intent(this@LoginActivity, nextActivity)
                        startActivity(intent)
                        finish()
                    }
                } else {
                    val errorMsg = response.body()?.userMsg ?: "Sai tài khoản hoặc mật khẩu"
                    Toast.makeText(this@LoginActivity, errorMsg, Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<LoginResponse>, t: Throwable) {
                binding.progressBar.visibility = View.GONE

                Toast.makeText(this@LoginActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
}
