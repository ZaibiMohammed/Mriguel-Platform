/**
 * This script generates a mock JWT token for testing purposes
 * Note: This is only for development/testing and should never be used in production
 */

// Mock user data
const mockUser = {
  sub: "00000000-0000-0000-0000-000000000001", // User ID
  nameid: "00000000-0000-0000-0000-000000000001", // User ID (alternative claim)
  unique_name: "test@example.com",
  email: "test@example.com",
  given_name: "Test",
  family_name: "User",
  role: "User",
  nbf: Math.floor(Date.now() / 1000),
  exp: Math.floor(Date.now() / 1000) + 3600, // Token expires in 1 hour
  iat: Math.floor(Date.now() / 1000),
  iss: "mriguel-api",
  aud: "mriguel-client"
};

// Function to base64 encode a string
function base64UrlEncode(str) {
  return btoa(str)
    .replace(/\+/g, '-')
    .replace(/\//g, '_')
    .replace(/=+$/, '');
}

// Create JWT header
const header = {
  alg: "HS256",
  typ: "JWT"
};

// Generate token
const encodedHeader = base64UrlEncode(JSON.stringify(header));
const encodedPayload = base64UrlEncode(JSON.stringify(mockUser));
const signature = "MOCK_SIGNATURE"; // In a real JWT, this would be a cryptographic signature
const mockToken = `${encodedHeader}.${encodedPayload}.${signature}`;

// Display the token
document.getElementById('token-display').textContent = mockToken;

// Copy to clipboard function
function copyToken() {
  const tokenText = document.getElementById('token-display').textContent;
  navigator.clipboard.writeText(tokenText)
    .then(() => {
      document.getElementById('copy-status').textContent = 'Token copied to clipboard!';
      setTimeout(() => {
        document.getElementById('copy-status').textContent = '';
      }, 2000);
    })
    .catch(err => {
      document.getElementById('copy-status').textContent = 'Failed to copy token: ' + err;
    });
}
