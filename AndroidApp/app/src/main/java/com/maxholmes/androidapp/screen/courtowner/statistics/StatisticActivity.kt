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

        // Giả lập dữ liệu Statistic
        val statistics = mutableListOf<Statistic>(
            Statistic(
                CourtCluster(
                    id = "278f0020-0ceb-461e-8ddd-57380d02170b",
                    name = "Sân Quảng An",
                    openingTime = "06:00 AM",
                    closingTime = "10:00 PM",
                    description = "Sân chơi cho các hoạt động thể thao",
                    addressId = "278f0020-0ceb-461e-8ddd-57380d02170b",
                    courtOwnerId = "owner123",
                    imageUrl = "https://example.com/image1.jpg"
                ),
                price = 2500000.0,
                bookingCount = 10
            ),
            Statistic(
                CourtCluster(
                    id = "2d112240-9285-4932-8d7f-b6f19eb969d9",
                    name = "Sân Lào Cai",
                    openingTime = "07:00 AM",
                    closingTime = "09:00 PM",
                    description = "Sân phục vụ cho các giải đấu thể thao",
                    addressId = "2d112240-9285-4932-8d7f-b6f19eb969d9",
                    courtOwnerId = "owner124",
                    imageUrl = "https://example.com/image2.jpg"
                ),
                price = 1500000.0,
                bookingCount = 15
            ),
            Statistic(
                CourtCluster(
                    id = "764af1d9-64ea-486f-b778-e102233a8d8b",
                    name = "Sân Kim Mã",
                    openingTime = "08:00 AM",
                    closingTime = "09:30 PM",
                    description = "Sân thảo luận và giao lưu thể thao",
                    addressId = "764af1d9-64ea-486f-b778-e102233a8d8b",
                    courtOwnerId = "owner125",
                    imageUrl = "https://example.com/image3.jpg"
                ),
                price = 1000000.0,
                bookingCount = 10
            ),
            Statistic(
                CourtCluster(
                    id = "e964d230-ef8e-4f53-b6ae-301dd8835c7b",
                    name = "Sân Cầu Giấy",
                    openingTime = "06:30 AM",
                    closingTime = "11:00 PM",
                    description = "Sân với tiện ích cao cấp",
                    addressId = "e964d230-ef8e-4f53-b6ae-301dd8835c7b",
                    courtOwnerId = "owner126",
                    imageUrl = "https://example.com/image4.jpg"
                ),
                price = 0.0,
                bookingCount = 0
            )
        )

        // Cập nhật dữ liệu cho adapter
        statisticAdapter.updateData(statistics)

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
