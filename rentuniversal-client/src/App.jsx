// App.jsx
import { useState } from "react";
import "./App.css";

function App() {
    const [page, setPage] = useState("home"); // "home" | "rental" | "cart" | "login"

    return (
        <div className="app">
            <Header currentPage={page} onChangePage={setPage} />

            <div className="layout">
                <Sidebar />

                <main className="main">
                    {page === "home" && <HomePage />}
                    {page === "rental" && <RentalPage />}
                    {page === "cart" && <CartPage />}
                    {page === "login" && <LoginPage />}
                </main>
            </div>

            <Footer />
        </div>
    );
}

// Top menu med de 4 knapper
function Header({ currentPage, onChangePage }) {
    return (
        <header className="header">
            <NavButton
                label="Home"
                active={currentPage === "home"}
                onClick={() => onChangePage("home")}
            />
            <NavButton
                label="Udlejning"
                active={currentPage === "rental"}
                onClick={() => onChangePage("rental")}
            />
            <NavButton
                label="Kurv"
                active={currentPage === "cart"}
                onClick={() => onChangePage("cart")}
            />
            <NavButton
                label="Login"
                active={currentPage === "login"}
                onClick={() => onChangePage("login")}
            />
        </header>
    );
}

function NavButton({ label, active, onClick }) {
    return (
        <button
            className={`nav-button ${active ? "nav-button-active" : ""}`}
            onClick={onClick}
        >
            {label}
        </button>
    );
}

// Venstre kategori-menu
function Sidebar() {
    return (
        <aside className="sidebar">
            <h3>Kategori</h3>
            <ul>
                <li>Værktøj</li>
                <li>Udendørs udstyr</li>
                <li>Fest-udstyr</li>
                <li>Brunekøler</li>
                <li>Heste-udstyr</li>
            </ul>
        </aside>
    );
}

// FOOTER
function Footer() {
    return (
        <footer className="footer">
            <div className="footer-logo">RentAll Logo</div>
            <div className="footer-links">
                <div className="footer-box">Om os</div>
                <div className="footer-box">Privatliv</div>
                <div className="footer-box">Support</div>
            </div>
        </footer>
    );
}

/* ---------- SIDER ---------- */

// HOME – “Du er på Home”
function HomePage() {
    return (
        <section>
            <h2>Velkommen til RentAll</h2>
            <div className="card-list">
                <ItemCard
                    title="Hammer"
                    price="200 kr. pr. dag"
                    location="Aalborg"
                />
                <ItemCard
                    title="Diskokugle"
                    price="150 kr. pr. dag"
                    location="Aarhus"
                />
            </div>
        </section>
    );
}

// UDLEJNING – “Du er på Udlejning”
function RentalPage() {
    return (
        <section>
            <h2>Udlejning</h2>
            <p>Her kan du se og vælge de ting, du vil leje.</p>

            <div className="card-list">
                <ItemCard
                    title="Murerhammer"
                    price="300 kr. pr. dag"
                    location="Aalborg"
                />
                <ItemCard
                    title="symaskine"
                    price="80 kr. pr. dag"
                    location="Odense"
                />
            </div>

            <h3>Dine kontrakter</h3>
            <div className="contract-list">
                <ContractCard
                    item="Hammer"
                    period="20/11 - 23/11"
                    price="200 kr. pr. dag"
                    status="Aktiv"
                />
                <ContractCard
                    item="Diskokugle"
                    period="01/12 - 02/12"
                    price="150 kr. pr. dag"
                    status="Aktiv"
                />
            </div>
        </section>
    );
}

// KURV – “Du er på Kurv”
function CartPage() {
    return (
        <section>
            <h2>Kurv</h2>

            <table className="cart-table">
                <thead>
                <tr>
                    <th>Vare</th>
                    <th>Antal</th>
                    <th>Pris</th>
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td>Hammer</td>
                    <td>
                        <select defaultValue={1}>
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                        </select>
                    </td>
                    <td>DKK 200</td>
                </tr>
                <tr>
                    <td>Symaskine</td>
                    <td>
                        <select defaultValue={2}>
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                        </select>
                    </td>
                    <td>DKK 600</td>
                </tr>
                </tbody>
            </table>

            <div className="cart-summary">
                <p>Total: DKK 800 inkl. moms</p>
                <p>Levering: Gratis</p>
                <button className="primary-btn">Betal</button>
            </div>
        </section>
    );
}

// LOGIN – “Du er på Login”
function LoginPage() {
    return (
        <section className="login-section">
            <h2>Login</h2>
            <form className="login-form">
                <label>
                    Email
                    <input type="email" placeholder="din@email.dk" />
                </label>
                <label>
                    Adgangskode
                    <input type="password" />
                </label>
                <button className="primary-btn" type="submit">
                    Log ind
                </button>
            </form>
        </section>
    );
}

/* ---------- SMÅ KOMPONENTER ---------- */

function ItemCard({ title, price, location }) {
    return (
        <div className="item-card">
            <div className="item-image">Billede</div>
            <div className="item-text">
                <h4>{title}</h4>
                <p>{price}</p>
                <p>Location: {location}</p>
            </div>
        </div>
    );
}

function ContractCard({ item, period, price, status }) {
    return (
        <div className="contract-card">
            <h4>{item}</h4>
            <p>Periode: {period}</p>
            <p>Pris: {price}</p>
            <p>Status: {status}</p>
        </div>
    );
}

export default App;
