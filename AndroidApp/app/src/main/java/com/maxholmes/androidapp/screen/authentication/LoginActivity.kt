package com.maxholmes.androidapp.screen.authentication

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.ProgressBar
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.LoginRequest
import com.maxholmes.androidapp.data.dto.response.LoginResponse
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.screen.customer.home.HomeCustomerActivity
import com.maxholmes.androidapp.screen.test.TestActivity
import com.maxholmes.androidapp.utils.ext.authentication.decodeJWT
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class LoginActivity : AppCompatActivity() {

    private lateinit var emailEditText: EditText
    private lateinit var passwordEditText: EditText
    private lateinit var loginButton: Button
    private lateinit var progressBar: ProgressBar

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        // Initialize views
        emailEditText = findViewById(R.id.email)
        passwordEditText = findViewById(R.id.password)
        loginButton = findViewById(R.id.loginButton)
        progressBar = findViewById(R.id.progressBar)

        // Set click listener for login button
        loginButton.setOnClickListener {
            val email = emailEditText.text.toString().trim()
            val password = passwordEditText.text.toString().trim()

            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Email and password are required", Toast.LENGTH_SHORT).show()
            } else {
                performLogin(email, password)
            }
        }
    }

    private fun performLogin(email: String, password: String) {
        // Show progress bar
        progressBar.visibility = View.VISIBLE

        // Create a login request
        val loginRequest = LoginRequest(username = email, password = password)

        // Make the login API call using Retrofit
        RetrofitClient.ApiClient.apiService.login(loginRequest).enqueue(object : Callback<LoginResponse> {
            override fun onResponse(call: Call<LoginResponse>, response: Response<LoginResponse>) {
                // Hide progress bar
                progressBar.visibility = View.GONE

                if (response.isSuccessful && response.body()?.success == true) {
                    val token = response.body()?.data?.token
                    Toast.makeText(this@LoginActivity, "Login successful", Toast.LENGTH_SHORT).show()

                    if (token != null) {
                        // Decode JWT and check role
                        val claims = decodeJWT(token)
                        val role = claims["role"] as String

                        val intent = if (role == "Customer") {
                            Intent(this@LoginActivity, HomeCustomerActivity::class.java)
                        } else {
                            Intent(this@LoginActivity, TestActivity::class.java)
                        }

                        // Pass token to the next activity
                        intent.putExtra("TOKEN", token)
                        startActivity(intent)
                        finish()
                    }
                } else {
                    val errorMsg = response.body()?.userMsg ?: "Login failed"
                    Toast.makeText(this@LoginActivity, errorMsg, Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<LoginResponse>, t: Throwable) {
                // Hide progress bar
                progressBar.visibility = View.GONE

                // Show error message
                Toast.makeText(this@LoginActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
}
