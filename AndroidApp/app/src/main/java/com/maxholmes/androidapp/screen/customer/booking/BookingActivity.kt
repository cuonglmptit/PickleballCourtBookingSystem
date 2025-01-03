package com.maxholmes.androidapp.screen.customer.booking

import android.os.Bundle
import android.widget.Toast
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.model.CourtTimeSlot
import com.maxholmes.androidapp.databinding.ActivityBookingBinding
import com.maxholmes.androidapp.screen.customer.booking.adapter.CourtTimeSlotAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectCourtAdapter
import com.maxholmes.androidapp.screen.customer.booking.adapter.SelectDayAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.Calendar

class BookingActivity : AppCompatActivity() {
    private lateinit var binding: ActivityBookingBinding
    private lateinit var selectCourtAdapter: SelectCourtAdapter
    private lateinit var selectDayAdapter: SelectDayAdapter
    lateinit var courtTimeSlotAdapter: CourtTimeSlotAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityBookingBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Setup RecyclerViews
        selectCourtAdapter = SelectCourtAdapter()
        selectDayAdapter = SelectDayAdapter()
        courtTimeSlotAdapter = CourtTimeSlotAdapter()

        binding.recyclerViewCourt.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewCourt.adapter = selectCourtAdapter

        binding.recyclerViewDay.layoutManager = LinearLayoutManager(this, LinearLayoutManager.HORIZONTAL, false)
        binding.recyclerViewDay.adapter = selectDayAdapter

        binding.recyclerViewTimeSlot.layoutManager = GridLayoutManager(this, 3, GridLayoutManager.VERTICAL, false)
        binding.recyclerViewTimeSlot.adapter = courtTimeSlotAdapter

        // Load data into adapters
        loadCourts()
        loadDays()
        loadTimeSlots()

        selectDayAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Calendar> {
            override fun onItemClick(item: Calendar?) {
                item?.let {
                    val position = selectDayAdapter.getSelectDays().indexOf(it)
                    selectDayAdapter.setSelectedItem(position)
//                    selectedDay = it
                }
            }
        })

        selectCourtAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<Court> {
            override fun onItemClick(item: Court?) {
                item?.let {
                    val position = selectCourtAdapter.getCourts().indexOf(it)
                    selectCourtAdapter.setSelectedItem(position)
//                    selectedCourt = it
                }
            }
        })

        courtTimeSlotAdapter.registerItemRecyclerViewClickListener(object : OnItemRecyclerViewClickListener<CourtTimeSlot> {
            override fun onItemClick(item: CourtTimeSlot?) {
                item?.let {
                    // Lấy vị trí của khung giờ trong danh sách
                    val position = courtTimeSlotAdapter.getCourtTimeSlots().indexOf(it)
                    if (position != RecyclerView.NO_POSITION) {
                        // Gọi toggleSelection để xử lý thay đổi trạng thái
                        courtTimeSlotAdapter.toggleSelection(position)
                    }
                }
            }
        })

    }

    private fun loadTimeSlots() {
        val timeSlots = mutableListOf<CourtTimeSlot>()
        val formatter = DateTimeFormatter.ofPattern("HH:mm")
        var currentTime = LocalTime.of(5, 0) // Bắt đầu từ 05:00

        for (i in 0 until 18) { // Tạo 18 khung giờ (từ 05:00 đến 22:00)
            val timeString = currentTime.format(formatter)
            timeSlots.add(CourtTimeSlot(
                id = "time_slot_$i",
                date = "2024-12-31", // Ngày 31/12/2024
                time = timeString,
                isAvailable = 1, // Giả sử tất cả các khung giờ đều có sẵn
                price = 100000.0, // Giá 100.000 VND
                courtId = "1" // Sân 1
            ))
            currentTime = currentTime.plusHours(1) // Tăng thêm 1 giờ
        }

        courtTimeSlotAdapter.updateData(timeSlots)
    }

    private fun loadDays() {
        val calendar = Calendar.getInstance()
        val selectDays = mutableListOf<Calendar>()

        for (i in 0..6) {
            val day = calendar.clone() as Calendar
            day.add(Calendar.DAY_OF_YEAR, i)
            selectDays.add(day)
        }

        selectDayAdapter.updateData(selectDays)
    }

    private fun loadCourts() {
        var courts = mutableListOf(
            Court(
                id = "test",
                courtNumber = 1,
                description = null,
                courtClusterId = "test"
            ),
            Court(
                id = "test",
                courtNumber = 2,
                description = null,
                courtClusterId = "test"
            )
        )
        selectCourtAdapter.updateData(courts)
    }
}
