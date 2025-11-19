import React, { useEffect, useState } from "react";
import { apiDelete, apiGet } from "../utils/api";
import InvoiceTable from "./InvoiceTable";

const InvoiceIndex = () => {
  const [invoices, setInvoices] = useState([]);
  const [filters, setFilters] = useState({
    year: "",
    product: "",
    minPrice: "",
    maxPrice: "",
  });

  //  Načtení faktur s použitím filtrů
  const loadInvoices = async (customFilters = filters) => {
    try {
      const query = new URLSearchParams(
        Object.entries(customFilters).filter(([_, v]) => v)
      ).toString();

      //  Přidáno: základní API URL, pokud není v apiGet
     const data = await apiGet("/api/invoices", customFilters);
      setInvoices(data);
    } catch (err) {
      console.error("Chyba při načítání faktur:", err);
    }
  };

  //  Smazání faktury
  const deleteInvoice = async (id) => {
    try {
      await apiDelete("/api/invoices/" + id);
      setInvoices(invoices.filter((item) => item._id !== id));
    } catch (error) {
      console.error(error.message);
      alert("Chyba při mazání faktury: " + error.message);
    }
  };

  useEffect(() => {
    loadInvoices();
  }, []);

  //  Změna hodnot filtrů
  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters({ ...filters, [name]: value });
  };

  //  Reset filtrů
  const resetFilters = () => {
    const cleared = { year: "", product: "", minPrice: "", maxPrice: "" };
    setFilters(cleared);
    //  Zavoláme loadInvoices až po resetu
    setTimeout(() => loadInvoices(cleared), 0);
  };

  return (
    <div>
      <h1>Seznam faktur</h1>

      {/*  Panel filtrů */}
      <div className="card p-3 mb-3">
        <h5>Filtr faktur</h5>
        <div className="row g-2">
          <div className="col-md-2">
            <input
              type="number"
              name="year"
              value={filters.year}
              onChange={handleFilterChange}
              placeholder="Rok"
              className="form-control"
            />
          </div>
          <div className="col-md-3">
            <input
              type="text"
              name="product"
              value={filters.product}
              onChange={handleFilterChange}
              placeholder="Produkt / služba"
              className="form-control"
            />
          </div>
          <div className="col-md-2">
            <input
              type="number"
              name="minPrice"
              value={filters.minPrice}
              onChange={handleFilterChange}
              placeholder="Minimální cena"
              className="form-control"
            />
          </div>
          <div className="col-md-2">
            <input
              type="number"
              name="maxPrice"
              value={filters.maxPrice}
              onChange={handleFilterChange}
              placeholder="Maximální cena"
              className="form-control"
            />
          </div>
          <div className="col-md-3 d-flex align-items-center">
            <button
              className="btn btn-primary me-2"
              onClick={() => loadInvoices()}
            >
               Filtrovat
            </button>
            <button className="btn btn-secondary" onClick={resetFilters}>
               Zrušit filtr
            </button>
          </div>
        </div>
      </div>

      {/*  Tabulka faktur */}
      <InvoiceTable
        deleteInvoice={deleteInvoice}
        items={invoices}
        label="Počet faktur:"
      />
    </div>
  );
};

export default InvoiceIndex;