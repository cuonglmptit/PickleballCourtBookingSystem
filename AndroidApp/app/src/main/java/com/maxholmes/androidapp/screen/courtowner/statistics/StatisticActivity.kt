package com.maxholmes.androidapp.screen.courtowner.statistics

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.data.model.Statistic
import com.maxholmes.androidapp.screen.courtowner.statistics.adapter.StatisticAdapter
import com.maxholmes.androidapp.databinding.ActivityStatisticBinding

class StatisticActivity : AppCompatActivity() {

    // Biến binding cho View Binding
    private lateinit var binding: ActivityStatisticBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Khởi tạo binding
        binding = ActivityStatisticBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Khởi tạo adapter
        val statisticAdapter = StatisticAdapter()


        // Cập nhật dữ liệu cho adapter
//        statisticAdapter.updateData(statistics)

        // Gán RecyclerView và Adapter
        binding.recyclerView.layoutManager = LinearLayoutManager(this)
        binding.recyclerView.adapter = statisticAdapter

        // Giả lập tổng doanh thu và tổng số lượt đặt
        binding.tvTotalRevenue.text = "Tổng doanh thu: 5.000.000₫"
        binding.tvTotalOrders.text = "Tổng số lượt đặt: 35"

        // Xử lý sự kiện cho button quay lại
        binding.backButton.setOnClickListener {
            finish()  // Đóng activity và quay lại màn hình trước
        }
    }
}
