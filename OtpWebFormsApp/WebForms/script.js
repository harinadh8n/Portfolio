let sentOtp = null; // Variable to store the sent OTP

async function sendOtp(event) {
    event.preventDefault();

    const mobile = document.getElementById('mobile').value;
    console.log("Sending OTP to:", mobile); // Debug log

    try {
        const response = await fetch('https://localhost:7174/api/otp/send', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Mobile: mobile })
        });

        if (!response.ok) {
            const errorText = await response.text(); // Get error message from response
            throw new Error(`HTTP error! Status: ${response.status}, Message: ${errorText}`);
        }

        const data = await response.json();
        console.log("OTP response:", data); // Debug log

        if (data.otp) { // Ensure this matches your API's response structure
            sentOtp = data.otp; // Store the sent OTP
            document.getElementById('message').innerText = 'OTP sent! Please check your mobile.';
            toggleForms(true); // Show validate form
        } else {
            document.getElementById('message').innerText = 'Failed to send OTP.';
        }
    } catch (error) {
        document.getElementById('message').innerText = 'Error sending OTP: ' + error.message;
        console.error('Error:', error);
    }
}

function validateOtp(event) {
    event.preventDefault();

    const otp = document.getElementById('otp').value;
    console.log("Validating OTP:", otp); // Debug log

    // Validate against the stored sent OTP
    if (otp === sentOtp) { // Compare with stored OTP
        alert("OTP validated successfully! Redirecting to home page...");
        window.location.href = "home.html"; // Redirect to home page
    } else {
        document.getElementById('message').innerText = 'Invalid OTP. Please try again.';
    }
}

function toggleForms(isOtpSent) {
    document.getElementById('validateOtpForm').style.display = isOtpSent ? 'block' : 'none';
    document.getElementById('otpForm').style.display = isOtpSent ? 'none' : 'block';
}