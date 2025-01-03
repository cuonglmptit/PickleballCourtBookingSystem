package com.maxholmes.androidapp.screen.customer.booking.confirmbooking

import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.yourapp.screen.customer.booking.adapter.CourtTimeSlotBookingAdapter
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.ext.toCustomDateFormat

import com.maxholmes.androidapp.databinding.ActivityConfirmBookingBinding

class ConfirmBookingActivity : AppCompatActivity() {

    private lateinit var binding: ActivityConfirmBookingBinding
    private lateinit var courtTimeSlotBookingAdapter: CourtTimeSlotBookingAdapter
    private val courtTimeSlots = mutableListOf(
        CourtTimeSlot(
            id = "test1",
            date = "2024-12-31".toCustomDateFormat(),
            time = "05:00",
            isAvailable = 1,
            price = "100000".toDouble(),
            courtId = "test"
        ),
        CourtTimeSlot(
            id = "test2",
            date = "2024-12-31".toCustomDateFormat(),
            time = "06:00",
            isAvailable = 1,
            price = "100000".toDouble(),
            courtId = "test"
        )
    )

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        // Sử dụng ViewBinding để ánh xạ view
        binding = ActivityConfirmBookingBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Set up RecyclerView
        courtTimeSlotBookingAdapter = CourtTimeSlotBookingAdapter()
        binding.recyclerViewTimeSlots.layoutManager = LinearLayoutManager(this)
        binding.recyclerViewTimeSlots.adapter = courtTimeSlotBookingAdapter
        courtTimeSlotBookingAdapter.updateData(courtTimeSlots)

        // Register Click Listener
        courtTimeSlotBookingAdapter.registerItemRecyclerViewClickListener(object :
            OnItemRecyclerViewClickListener<CourtTimeSlot> {
            override fun onItemClick(item: CourtTimeSlot?) {
                item?.let {
                    Toast.makeText(
                        this@ConfirmBookingActivity,
                        "Đã chọn: ${item.time} - ${item.price}",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }
        })

        // Populate customer and court info
        binding.customerName.text = "Hoàng Minh Đức"
        binding.customerPhone.text = "034567891"
        binding.customerEmail.text = "maxholmes@gmail.com"
        binding.courtName.text = "Sân Sao Mai"
        binding.courtAddress.text = "Đường Xuân Thủy, Phường Dịch Vọng, Quận Cầu Giấy, Hà Nội"
        binding.courtPhone.text = "0123456789"
        binding.totalPrice.text = "200.000₫"
    }
}
