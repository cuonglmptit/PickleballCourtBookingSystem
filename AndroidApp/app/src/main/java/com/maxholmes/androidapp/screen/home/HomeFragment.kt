package com.maxholmes.androidapp.screen.home

import android.os.Bundle
import android.view.LayoutInflater
import android.widget.Toast
import androidx.recyclerview.widget.GridLayoutManager
import androidx.recyclerview.widget.LinearLayoutManager
import com.maxholmes.androidapp.R
import com.maxholmes.androidapp.data.model.Court
import com.maxholmes.androidapp.data.repository.CourtRepository
import com.maxholmes.androidapp.databinding.FragmentHomeBinding
import com.maxholmes.androidapp.screen.home.adapter.CourtAdapter
import com.maxholmes.androidapp.utils.OnItemRecyclerViewClickListener
import com.maxholmes.androidapp.utils.base.BaseFragment

class HomeFragment :
    BaseFragment<FragmentHomeBinding>(),
    HomeContract.View,
    OnItemRecyclerViewClickListener<Court> {
    private lateinit var mHomePresenter: HomePresenter
    private val mCourtAdapter: CourtAdapter by lazy { CourtAdapter() }

    override fun inflateViewBinding(inflater: LayoutInflater): FragmentHomeBinding {
        return FragmentHomeBinding.inflate(inflater)
    }

    override fun initView() {
        viewBinding.recyclerViewCourt.apply {
            layoutManager = GridLayoutManager(context, 2)
            adapter = mCourtAdapter
        }
        mCourtAdapter.registerItemRecyclerViewClickListener(this)
        setupBottomNavigation()
    }

    override fun initData() {
        mHomePresenter =
            HomePresenter(
                mCourtRepository = CourtRepository()
            )
        mHomePresenter.setView(this)

        val fakeCourts = mutableListOf(
            Court(
                id = "1",
                name = "Sân bóng 1",
                description = "Sân bóng đá mini, chất lượng cao.",
                length = 30f,
                width = 15f,
                addressId = "abc123",
                courtOwnerId = "abc123",
                imageUrl = "https://cdn.coinranking.com/rk4RKHOuW/eth.png"
            ),
            Court(
                id = "2",
                name = "Sân bóng 2",
                description = "Sân bóng đá với mặt cỏ nhân tạo.",
                length = 30f,
                width = 15f,
                addressId = "abc123",
                courtOwnerId = "abc123",
                imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTYq54MAgogO9JkyTDaeTJj887VQ_ymqbayTw&s"
            ),
            Court(
                id = "3",
                name = "Sân bóng 3",
                description = "Sân bóng đá cỏ tự nhiên, rộng rãi.",
                length = 40f,
                width = 20f,
                addressId = "abc123",
                courtOwnerId = "abc123",
                imageUrl = "https://pickleballsuperstore.com/cdn/shop/articles/pickleball_court_dimensions_top_1000x.jpg"
            ),
            Court(
                id = "4",
                name = "Sân bóng 4",
                description = "Sân bóng đá cỏ tự nhiên, chất lượng cao, phù hợp cho các giải đấu.",
                length = 50f,
                width = 25f,
                addressId = "xyz456",
                courtOwnerId = "xyz456",
                imageUrl = "https://5.imimg.com/data5/SELLER/Default/2024/2/393520434/WI/DO/BJ/37460891/pickle-ball-court-500x500.jpg"
            ),
            Court(
                id = "5",
                name = "Sân bóng 5",
                description = "Sân bóng đá mini với mặt cỏ nhân tạo, khu vực chờ thoải mái.",
                length = 28f,
                width = 14f,
                addressId = "xyz456",
                courtOwnerId = "xyz456",
                imageUrl = "https://5.imimg.com/data5/SELLER/Default/2024/6/426038716/EO/HA/VW/32771573/pickle-ball-court-500x500.jpg"
            ),
            Court(
                id = "6",
                name = "Sân bóng 6",
                description = "Sân bóng đá cỏ tự nhiên, với sân chơi cho trẻ em bên cạnh.",
                length = 35f,
                width = 18f,
                addressId = "xyz456",
                courtOwnerId = "xyz456",
                imageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9xZM4zlE4uWhBAB4EOv59pqLDqk5OXZUIEA&s"
            ),
//            Court(
//                id = "7",
//                name = "Sân bóng 7",
//                description = "Sân bóng đá với mặt cỏ nhân tạo, tiện nghi đầy đủ cho đội bóng.",
//                length = 32f,
//                width = 16f,
//                addressId = "xyz456",
//                courtOwnerId = "xyz456",
//                imageUrl = "https://example.com/images/court7.jpg"
//            ),
//            Court(
//                id = "8",
//                name = "Sân bóng 8",
//                description = "Sân bóng đá lớn, có thể tổ chức các giải đấu chuyên nghiệp.",
//                length = 55f,
//                width = 30f,
//                addressId = "xyz456",
//                courtOwnerId = "xyz456",
//                imageUrl = "https://example.com/images/court8.jpg"
//            )
        )
        mCourtAdapter.updateData(fakeCourts)
    }

//    override fun onGetCourtSuccess(courts: MutableList<Court>) {
//        mCourtAdapter.updateData(courts)
//    }

    override fun onError(exception: Exception?) {
        Toast.makeText(context, exception?.message, Toast.LENGTH_SHORT).show()
    }

    override fun onItemClick(item: Court?) {
        if (item != null) {
            val bundle =
                Bundle().apply {
                    putParcelable("COURT", item)
                }
//            parentFragmentManager.beginTransaction()
//                .replace(R.id.layoutContainer, DetailFragment.newInstance(item))
//                .addToBackStack(null)
//                .commit()
        } else {
            Toast.makeText(requireContext(), "Selected item is null", Toast.LENGTH_SHORT).show()
        }
    }

    private fun setupBottomNavigation() {
        viewBinding.bottomNavigationView.setOnItemSelectedListener {
            when (it.itemId) {
                R.id.home -> {
                    Toast.makeText(context, "Home", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.booking -> {
                    Toast.makeText(context, "Booking", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.notification -> {
                    Toast.makeText(context, "Notification", Toast.LENGTH_SHORT).show()
                    true
                }

                R.id.user -> {
                    Toast.makeText(context, "Settings", Toast.LENGTH_SHORT).show()
                    true
                }

                else -> false
            }
        }
    }

    companion object {
        fun newInstance() = HomeFragment()
    }
}