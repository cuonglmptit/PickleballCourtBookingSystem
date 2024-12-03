USE `freedb_PickleBallCourtBookingSystem`;

CREATE INDEX idx_user_addressId ON User(addressId);
CREATE INDEX idx_user_roleId_isActive ON User(roleId, isActive);

CREATE INDEX idx_courtOwner_userId ON CourtOwner(userId);
CREATE INDEX idx_courtCluster_addressId ON CourtCluster(addressId);
CREATE INDEX idx_courtCluster_courtOwnerId ON CourtCluster(courtOwnerId);

CREATE INDEX idx_courtPrice_courtClusterId ON CourtPrice(courtClusterId);

CREATE INDEX idx_court_courtClusterId ON Court(courtClusterId);

CREATE INDEX idx_imageCourtURL_courtClusterId ON ImageCourtURL(courtClusterId);

CREATE INDEX idx_courtTimeSlot_courtId ON CourtTimeSlot(courtId);
CREATE INDEX idx_isAvailable_date_time ON CourtTimeSlot(isAvailable, date, time);

CREATE INDEX idx_customer_userId ON Customer(userId);

CREATE INDEX idx_admin_userId ON Admin(userId);

CREATE INDEX idx_booking_courtId ON Booking(courtId);
CREATE INDEX idx_booking_courtClusterId ON Booking(courtClusterId);
CREATE INDEX idx_booking_customerId ON Booking(customerId);
CREATE INDEX idx_booking_status_paymentStatus ON Booking(status, paymentStatus);

CREATE INDEX idx_courtTimeBooking_bookingId ON CourtTimeBooking(bookingId);
CREATE INDEX idx_courtTimeBooking_courtTimeSlotId ON CourtTimeBooking(courtTimeSlotId);

CREATE INDEX idx_feedback_bookingId ON Feedback(bookingId);
CREATE INDEX idx_feedback_courtClusterId ON Feedback(courtClusterId);
CREATE INDEX idx_feedback_customerId ON Feedback(customerId);
CREATE INDEX idx_feedback_rating ON Feedback(rating);

CREATE INDEX idx_cancellation_bookingId ON Cancellation(bookingId);
