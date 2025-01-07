package com.maxholmes.androidapp.screen.register

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.data.dto.request.RegisterRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityRegisterBinding
import com.maxholmes.androidapp.screen.authentication.LoginActivity
import com.maxholmes.androidapp.screen.test.TestActivity
import com.maxholmes.androidapp.utils.enum.RoleEnum
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class RegisterActivity : AppCompatActivity() {

    private lateinit var binding: ActivityRegisterBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegisterBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setupListeners()
    }

    private fun setupListeners() {
        binding.checkboxCustomer.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked) {
                binding.checkboxOwner.isEnabled = false
                binding.checkboxOwner.isChecked = false
            } else {
                binding.checkboxOwner.isEnabled = true
            }
        }

        binding.checkboxOwner.setOnCheckedChangeListener { _, isChecked ->
            if (isChecked) {
                binding.checkboxCustomer.isEnabled = false
                binding.checkboxCustomer.isChecked = false
            } else {
                binding.checkboxCustomer.isEnabled = true
            }
        }

        binding.btnRegister.setOnClickListener {
            val username = binding.edtUsername.text.toString().trim()
            val fullName = binding.edtFullName.text.toString().trim()
            val email = binding.edtEmail.text.toString().trim()
            val phoneNumber = binding.edtPhoneNumber.text.toString().trim()
            val password = binding.edtPassword.text.toString()
            val confirmPassword = binding.edtConfirmPassword.text.toString()

            if (username.isEmpty() || email.isEmpty() || phoneNumber.isEmpty() ||
                password.isEmpty() || confirmPassword.isEmpty()) {
                Toast.makeText(this, "Vui lòng nhập đầy đủ thông tin", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            if (password != confirmPassword) {
                Toast.makeText(this, "Mật khẩu không khớp", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val role = when {
                binding.checkboxCustomer.isChecked -> RoleEnum.Customer
                binding.checkboxOwner.isChecked -> RoleEnum.CourtOwner
                else -> {
                    Toast.makeText(this, "Vui lòng chọn loại tài khoản", Toast.LENGTH_SHORT).show()
                    return@setOnClickListener
                }
            }

            val registerRequest = RegisterRequest(
                username = username,
                password = password,
                confirmPassword = confirmPassword,
                name = fullName,
                email = email,
                phoneNumber = phoneNumber,
                role = role
            )

            register(registerRequest)
        }

        binding.tvLogin.setOnClickListener {
            val intent = Intent(this, LoginActivity::class.java)
            startActivity(intent)
        }
    }

    private fun register(request: RegisterRequest) {
        binding.progressBar.visibility = View.VISIBLE
        val apiService = RetrofitClient.ApiClient.apiService
        apiService.register(request).enqueue(object : Callback<APIResponse> {
            override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                if (response.isSuccessful) {
                    val apiResponse = response.body()
                    if (apiResponse?.success == true) {
                        binding.progressBar.visibility = View.GONE
                        Toast.makeText(this@RegisterActivity, "Đăng ký thành công!", Toast.LENGTH_SHORT).show()
                        val intent = Intent(this@RegisterActivity, LoginActivity::class.java)
                        startActivity(intent)
                        finish()
                    } else {
                        binding.progressBar.visibility = View.GONE
                    Toast.makeText(this@RegisterActivity, apiResponse?.userMsg ?: "Lỗi đăng ký, vui lòng đăng ký lại", Toast.LENGTH_SHORT).show()
                        val intent = Intent(this@RegisterActivity, RegisterActivity::class.java)
                        startActivity(intent)
                    }
                } else {
                    binding.progressBar.visibility = View.GONE
                    Toast.makeText(this@RegisterActivity, "Lỗi: ${response.message()}", Toast.LENGTH_SHORT).show()
                    val intent = Intent(this@RegisterActivity, TestActivity::class.java)
                    startActivity(intent)
                }
            }

            override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                binding.progressBar.visibility = View.GONE
                Toast.makeText(this@RegisterActivity, "Lỗi kết nối: ${t.message}", Toast.LENGTH_SHORT).show()
                val intent = Intent(this@RegisterActivity, TestActivity::class.java)
                startActivity(intent)
            }
        })
    }
}
