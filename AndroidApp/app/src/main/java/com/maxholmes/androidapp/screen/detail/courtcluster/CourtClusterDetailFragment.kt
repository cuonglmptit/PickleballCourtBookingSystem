package com.maxholmes.androidapp.screen.detail.courtcluster

import android.view.LayoutInflater
import android.widget.Toast
import com.maxholmes.androidapp.data.model.CourtCluster
import com.maxholmes.androidapp.databinding.FragmentCourtClusterDetailBinding
import com.maxholmes.androidapp.utils.base.BaseFragment

class CourtClusterDetailFragment :
    BaseFragment<FragmentCourtClusterDetailBinding>(), CourtClusterDetailContract.View {

    override fun inflateViewBinding(inflater: LayoutInflater): FragmentCourtClusterDetailBinding {
        return FragmentCourtClusterDetailBinding.inflate(inflater)
    }

    override fun initView() {
        val itemCourtCluster = arguments?.getParcelable("COURT_CLUSTER", CourtCluster::class.java)
        itemCourtCluster?.let {
            viewBinding.courtNameTextView.text = it.name
            viewBinding.courtDescriptionTextView.text = it.description
//            viewBinding.addressCourtTextView.text = it.addressId
        } ?: run {
            Toast.makeText(requireContext(), "Du lieu san bi loi", Toast.LENGTH_SHORT).show()
        }

    }

    override fun initData() {
    }

    override fun onError(exception: Exception?) {
        TODO("Not yet implemented")
    }

    companion object {
        fun newInstance(item: CourtCluster) = CourtClusterDetailFragment()
    }
}