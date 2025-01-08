package com.maxholmes.androidapp.screen.courtowner.add

import DateAdapter
import android.app.DatePickerDialog
import android.content.Intent
import android.os.Bundle
import android.widget.DatePicker
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.request.AutoAddCourtTimeSlotRequest
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityAddCourtTimeSlotBinding
import com.maxholmes.androidapp.screen.courtowner.detail.courtprice.ManagePriceActivity
import com.maxholmes.androidapp.utils.ext.SharedPreferencesUtils
import com.maxholmes.androidapp.utils.ext.formatDateToRequest
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.util.*

class AddCourtTimeSlotActivity : AppCompatActivity() {

    private lateinit var binding: ActivityAddCourtTimeSlotBinding
    private var listDate: MutableList<String> = mutableListOf()
    private lateinit var courtCluster: CourtCluster


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityAddCourtTimeSlotBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val token = SharedPreferencesUtils.getToken(this)
        courtCluster = intent.getParcelableExtra("courtCluster", CourtCluster::class.java)!!
        val courtClusterId = courtCluster.id
        binding.dateRecyclerView.layoutManager = LinearLayoutManager(this)
        binding.dateRecyclerView.adapter = DateAdapter(listDate)

        binding.selectDayButton.setOnClickListener {
            showDatePickerDialog()
        }

        binding.manageCourtPriceButton.setOnClickListener {
            val intent = Intent(this, ManagePriceActivity::class.java)
            intent.putExtra("courtClusterId", courtClusterId)
            startActivity(intent)
        }

        binding.confirmButton.setOnClickListener {
            addCourtTimeSlots(token!!)
        }
    }

    private fun showDatePickerDialog() {
        val calendar = Calendar.getInstance()
        val year = calendar.get(Calendar.YEAR)
        val month = calendar.get(Calendar.MONTH)
        val day = calendar.get(Calendar.DAY_OF_MONTH)
        calendar.add(Calendar.DAY_OF_YEAR, 1)

        val datePickerDialog = DatePickerDialog(
            this,
            { _: DatePicker, selectedYear: Int, selectedMonth: Int, selectedDay: Int ->
                val selectedDateFormatted = String.format("%02d/%02d/%04d", selectedDay, selectedMonth + 1, selectedYear)
                val formattedDateForRequest = selectedDateFormatted.formatDateToRequest()

                if (!listDate.contains(formattedDateForRequest)) {
                    listDate.add(formattedDateForRequest)
                    binding.dateRecyclerView.adapter?.notifyDataSetChanged()
                } else {
                    showToast("Ngày này đã được chọn.")
                }
            },
            year,
            month,
            day
        )

        datePickerDialog.datePicker.minDate = calendar.timeInMillis
        datePickerDialog.show()
    }

    private fun addCourtTimeSlots(token: String) {
        if (listDate.isEmpty()) {
            showToast("Vui lòng chọn ít nhất một ngày.")
            return
        }

        val request = AutoAddCourtTimeSlotRequest(courtCluster.id, listDate)

        RetrofitClient.ApiClient.apiService.autoCreateCourtTimeSlot(request, "Bearer $token")
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        showToast("Đã thêm slot đặt sân thành công.")
                        finish()
                    } else {
                        showToast("Lỗi khi thêm slot đặt sân.")
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    showToast("Lỗi kết nối mạng. Vui lòng thử lại!")
                }
            })
    }

    private fun showToast(message: String) {
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show()
    }
}
