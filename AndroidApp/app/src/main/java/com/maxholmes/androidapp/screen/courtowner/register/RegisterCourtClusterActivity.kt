package com.maxholmes.androidapp.screen.courtowner.register

import android.content.Intent
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.AddCourtClusterRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityRegisterCourtClusterBinding
import com.maxholmes.androidapp.screen.courtowner.home.HomeCourtOwnerActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class RegisterCourtClusterActivity : AppCompatActivity() {

    private lateinit var binding: ActivityRegisterCourtClusterBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegisterCourtClusterBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val timeList = generateTimeList()
        val timeAdapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, timeList)
        timeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)

        binding.openTimeSpinner.adapter = timeAdapter
        binding.closeTimeSpinner.adapter = timeAdapter

        binding.createButton.setOnClickListener {
            val name = binding.fieldName.text.toString().trim()
            val city = binding.cityField.text.toString().trim()
            val district = binding.districtField.text.toString().trim()
            val ward = binding.wardField.text.toString().trim()
            val street = binding.addressField.text.toString().trim()
            val numberOfCourts = binding.numberOfFields.text.toString().toIntOrNull() ?: 0
            val description = binding.descriptionField.text.toString().trim()
            val openingTime = binding.openTimeSpinner.selectedItem.toString()
            val closingTime = binding.closeTimeSpinner.selectedItem.toString()

            if (name.isEmpty() || city.isEmpty() || district.isEmpty() || ward.isEmpty() || street.isEmpty() || numberOfCourts == 0) {
                Toast.makeText(this, "Vui lòng điền đầy đủ thông tin!", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val token = SharedPreferencesUtils.getToken(this)
            if (token.isNullOrEmpty()) {
                Toast.makeText(this, "Token không hợp lệ!", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val addCourtClusterRequest = AddCourtClusterRequest(
                name = name,
                description = description,
                openingTime = openingTime,
                closingTime = closingTime,
                city = city,
                district = district,
                ward = ward,
                street = street,
                numberOfCourts = numberOfCourts
            )

            val tokenHeader = "Bearer $token"
            RetrofitClient.ApiClient.apiService.addCourtCluster(addCourtClusterRequest, tokenHeader)
                .enqueue(object : Callback<APIResponse> {
                    override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                        if (response.isSuccessful) {
                            Toast.makeText(this@RegisterCourtClusterActivity, "Đăng ký cụm sân thành công!", Toast.LENGTH_SHORT).show()
                            val intent = Intent(this@RegisterCourtClusterActivity, HomeCourtOwnerActivity::class.java)
                            startActivity(intent)
                            finish()
                        } else {
                            Toast.makeText(this@RegisterCourtClusterActivity, "Lỗi khi đăng ký cụm sân!", Toast.LENGTH_SHORT).show()
                        }
                    }

                    override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                        Toast.makeText(this@RegisterCourtClusterActivity, "Lỗi kết nối mạng. Vui lòng thử lại!", Toast.LENGTH_SHORT).show()
                    }
                })
        }
    }

    private fun generateTimeList(): List<String> {
        val timeList = mutableListOf<String>()
        for (hour in 5..22) {
            for (minute in listOf("00")) {
                timeList.add(String.format("%02d:%s:00", hour, minute))
            }
        }
        return timeList
    }
}
