package com.maxholmes.androidapp.screen.courtowner.detail.courtprice

import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.dto.response.APIResponse
import com.maxholmes.androidapp.data.dto.response.parseApiResponseData
import com.maxholmes.androidapp.data.model.CourtPrice
import com.maxholmes.androidapp.data.service.RetrofitClient
import com.maxholmes.androidapp.databinding.ActivityManagePriceBinding
import com.maxholmes.androidapp.utils.ext.formatTimeToStringWithSecond
import com.maxholmes.androidapp.utils.ext.generateTimeList
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ManagePriceActivity : AppCompatActivity() {

    private lateinit var binding: ActivityManagePriceBinding
    private lateinit var courtPriceAdapter: CourtPriceAdapter
    private var courtClusterId: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityManagePriceBinding.inflate(layoutInflater)
        setContentView(binding.root)

        courtClusterId = intent.getStringExtra("courtClusterId")!!
        initViews()
        loadCourtPrices()
        setupRecyclerView()

        binding.btnAddNew.setOnClickListener {
            addCourtPrice()
        }

        binding.backButton.setOnClickListener{
            onBackPressedDispatcher.onBackPressed()
        }

        binding.updateButton.setOnClickListener {
            updateCourtPrices()
            onBackPressedDispatcher.onBackPressed()
        }
    }

    private fun initViews() {
        val timeList = generateTimeList()
        val spinnerAdapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, timeList)
        spinnerAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.spinnerTimeAdd.adapter = spinnerAdapter
    }

    private fun setupRecyclerView() {
        courtPriceAdapter = CourtPriceAdapter()
        binding.recyclerViewCourtPrice.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewCourtPrice.adapter = courtPriceAdapter
        courtPriceAdapter.setOnDeleteClickListener { courtPrice ->
            onDeleteClicked(courtPrice)
        }
    }

    private fun loadCourtPrices() {
        RetrofitClient.ApiClient.apiService.getCourtPricesByCourtClusterId(courtClusterId)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        response.body()?.let { apiResponse ->
                            val courtPrices = parseApiResponseData<List<CourtPrice>>(apiResponse.data)
                            courtPrices?.let {
                                courtPriceAdapter.updateData(it.toMutableList())
                            }
                        }

                    } else {
                        showToast("Lỗi khi tải danh sách giá sân.")
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    showToast("Lỗi kết nối mạng. Vui lòng thử lại!")
                }
            })
    }

    private fun addCourtPrice() {
        val selectedTime = binding.spinnerTimeAdd.selectedItem.toString()
        val priceText = binding.tvPriceAdd.text.toString()

        if (priceText.isEmpty()) {
            showToast("Vui lòng nhập giá sân.")
            return
        }

        val price = priceText.toDoubleOrNull()
        if (price == null) {
            showToast("Giá sân không hợp lệ.")
            return
        }

        val newCourtPrice = CourtPrice(id = null, time = selectedTime.formatTimeToStringWithSecond(), price =  price, courtClusterId =  courtClusterId)

        if (courtPriceAdapter.contains(newCourtPrice)) {
            showToast("Thời gian này đã tồn tại.")
            return
        }

        courtPriceAdapter.addCourtPrice(newCourtPrice)
        showToast("Đã thêm giá sân.")
    }


    private fun updateCourtPrices() {
        val updatedPrices = courtPriceAdapter.getData()
        RetrofitClient.ApiClient.apiService.postManyCourtPrice(updatedPrices)
            .enqueue(object : Callback<APIResponse> {
                override fun onResponse(call: Call<APIResponse>, response: Response<APIResponse>) {
                    if (response.isSuccessful) {
                        showToast("Cập nhật giá sân thành công.")
                    } else {
                        showToast("Lỗi khi cập nhật giá sân.")
                    }
                }

                override fun onFailure(call: Call<APIResponse>, t: Throwable) {
                    showToast("Lỗi kết nối mạng. Vui lòng thử lại!")
                }
            })
    }

    private fun onDeleteClicked(courtPrice: CourtPrice) {
        courtPriceAdapter.removeCourtPrice(courtPrice)
    }

    private fun showToast(message: String) {
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show()
    }
}
